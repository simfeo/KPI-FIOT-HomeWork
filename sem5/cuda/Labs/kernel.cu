
#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <stdio.h>
#include <iostream>
#include <fstream>
#include <time.h>
#include <numeric>
#include <random>
#include <ctime>
#include <chrono>

cudaError_t addWithCuda(int *c, const int *a, const int *b, unsigned int size);

__global__ void addKernel(int *c, const int *a, const int *b)
{
    int i = threadIdx.x;
    c[i] = a[i]*3 + b[i] -8;
}

int main(int argc, char** argv)
{
	// generate 2 <<30 random integers

	const int CH_BUF_LEN = 32*32;

	
	std::ifstream infileA("file_a.txt", std::ifstream::binary);
	if (!infileA.is_open())
	{
		std::cerr << "no file test_a.txt" << std::endl;
		return 1;
	}

	std::ifstream infileB("file_b.txt", std::ifstream::binary);
	if (!infileB.is_open())
	{
		std::cerr << "no file test_b.txt" << std::endl;
		return 2;
	}

	std::ofstream outfile("new.txt", std::ofstream::binary);
	if (!outfile.is_open())
	{
		std::cerr << "cannot create new.txt" << std::endl;
		return 2;
	}


	cudaError_t cudaStatus = cudaSetDevice(0);
	if (cudaStatus != cudaSuccess) {
		std::cerr<< "cudaSetDevice failed!  Do you have a CUDA-capable GPU installed?" <<std::endl;
		return 12;
	}

	auto start = std::chrono::system_clock::now();
	std::time_t start_time = std::chrono::system_clock::to_time_t(start);
#pragma warning(suppress : 4996)
	std::cout << "start at " << std::ctime(&start_time) << std::endl;

	// get size of file
	infileA.ignore(std::numeric_limits<std::streamsize>::max());
	std::streamsize sizeA = infileA.gcount();
	infileA.clear();   //  Since ignore will have set eof.
	infileA.seekg(0, std::ios_base::beg);

	infileB.ignore(std::numeric_limits<std::streamsize>::max());
	std::streamsize sizeB = infileA.gcount();
	infileB.clear();   //  Since ignore will have set eof.
	infileB.seekg(0, std::ios_base::beg);

	if (sizeA != sizeB)
	{
		std::cerr << "file_a.txt size not match file_b.txt size" << std::endl;
		return 4;
	}

	int tmpBufA[CH_BUF_LEN] = { 0 };
	int tmpBufB[CH_BUF_LEN] = { 0 };
	int tmpBufC[CH_BUF_LEN] = { 0 };


	for (int i = 0; i < sizeA; i += CH_BUF_LEN * sizeof(int))
	{
		infileA.seekg(i);
		infileB.seekg(i);
		// read content of infile
		infileA.read((char*)tmpBufA, CH_BUF_LEN * sizeof(int));
		infileA.read((char*)tmpBufB, CH_BUF_LEN * sizeof(int));

		// write to outfile

		// Add vectors in parallel.
		cudaStatus = addWithCuda(tmpBufC, tmpBufA , tmpBufC, CH_BUF_LEN);
		if (cudaStatus != cudaSuccess) {
			std::cout << "addWithCuda failed!" << std::endl;;
			return 1;
		}	

		outfile.write((char*)tmpBufC, CH_BUF_LEN * sizeof(int));
	}

	cudaStatus = cudaDeviceReset();
	if (cudaStatus != cudaSuccess) {
		std::cerr << "cudaDeviceReset failed!" << std::endl;
		return 1;
	}

	outfile.close();
	//infileA.close();
	//infileB.close();

	auto end = std::chrono::system_clock::now();
	std::time_t end_time = std::chrono::system_clock::to_time_t(end);
	std::chrono::duration<double> elapsed_seconds = end - start;
#pragma warning(suppress : 4996)
	std::cout << "finished at " << std::ctime(&end_time) << "elapsed time: " << elapsed_seconds.count() << "s" << std::endl;

    return 0;
}

// Helper function for using CUDA to add vectors in parallel.
cudaError_t addWithCuda(int *c, const int *a, const int *b, unsigned int size)
{
    int *dev_a = 0;
    int *dev_b = 0;
    int *dev_c = 0;
    cudaError_t cudaStatus;

    // Allocate GPU buffers for three vectors (two input, one output)    .
    cudaStatus = cudaMalloc((void**)&dev_c, size * sizeof(int));
    if (cudaStatus != cudaSuccess) {
        fprintf(stderr, "cudaMalloc failed!");
        goto Error;
    }

    cudaStatus = cudaMalloc((void**)&dev_a, size * sizeof(int));
    if (cudaStatus != cudaSuccess) {
        fprintf(stderr, "cudaMalloc failed!");
        goto Error;
    }

    cudaStatus = cudaMalloc((void**)&dev_b, size * sizeof(int));
    if (cudaStatus != cudaSuccess) {
        fprintf(stderr, "cudaMalloc failed!");
        goto Error;
    }

    // Copy input vectors from host memory to GPU buffers.
    cudaStatus = cudaMemcpy(dev_a, a, size * sizeof(int), cudaMemcpyHostToDevice);
    if (cudaStatus != cudaSuccess) {
        fprintf(stderr, "cudaMemcpy failed!");
        goto Error;
    }

    cudaStatus = cudaMemcpy(dev_b, b, size * sizeof(int), cudaMemcpyHostToDevice);
    if (cudaStatus != cudaSuccess) {
        fprintf(stderr, "cudaMemcpy failed!");
        goto Error;
    }

    // Launch a kernel on the GPU with one thread for each element.
    addKernel<<<256, size>>>(dev_c, dev_a, dev_b);

    // Check for any errors launching the kernel
    cudaStatus = cudaGetLastError();
    if (cudaStatus != cudaSuccess) {
        fprintf(stderr, "addKernel launch failed: %s\n", cudaGetErrorString(cudaStatus));
        goto Error;
    }
    
    // cudaDeviceSynchronize waits for the kernel to finish, and returns
    // any errors encountered during the launch.
    cudaStatus = cudaDeviceSynchronize();
    if (cudaStatus != cudaSuccess) {
        fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
        goto Error;
    }

    // Copy output vector from GPU buffer to host memory.
    cudaStatus = cudaMemcpy(c, dev_c, size * sizeof(int), cudaMemcpyDeviceToHost);
    if (cudaStatus != cudaSuccess) {
        fprintf(stderr, "cudaMemcpy failed!");
        goto Error;
    }

Error:
    cudaFree(dev_c);
    cudaFree(dev_a);
    cudaFree(dev_b);
    
    return cudaStatus;
}
