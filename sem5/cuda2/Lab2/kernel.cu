
#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <stdio.h>
#include <iostream>

#include "cpu_anim.h"

#define DIMX 360*2
#define DIMY 270*2

#define W DIMX //i have a large display so mul2
#define H DIMY
#define MAXX 3.1415628*2 // Масштаб по оси X - 2*PI
#define MAXY 1,5
#define MINY -1
#define DT 0.2


__global__ void kernel(unsigned char *ptr, int ticks)
{
	int x = threadIdx.x + blockIdx.x * blockDim.x;
	int y = threadIdx.y + blockIdx.y * blockDim.y;
	int offset = x + y * blockDim.x * gridDim.x;

	float c0, c1, c2;

	c0 = abs(H / (MAXY - MINY)*(4*cos(tan((x + 0.0) / W * (MAXX + ticks * DT))) - MINY) - y);
	c1 = abs(H / (MAXY - MINY)*(4*cos(tan((x + 0.5) / W * (MAXX + ticks * DT))) - MINY) - y);
	c2 = abs(H / (MAXY - MINY)*(4*cos(tan((x - 0.5) / W * (MAXX + ticks * DT))) - MINY) - y);

	if (c0 <= 1 || c1 <= 1 || c2 <= 1)
		ptr[offset * 4 + 1] = ptr[offset * 4 + 2] = 0;
	else
		ptr[offset * 4 + 1] = ptr[offset * 4 + 2] = 255;

	ptr[offset * 4 + 0] = 255;
	ptr[offset * 4 + 3] = 255;
}

struct DataBlock
{
	unsigned char *dev_bitmap;
	CPUAnimBitmap *bitmap;
};

// Освободить выделенную память устройства
void cleanup(DataBlock *d)
{
	cudaFree(d->dev_bitmap);
}

void generate_frame(DataBlock *d, int ticks)
{
	dim3 blocks(DIMX / 16, DIMY / 16);
	dim3 threads(16, 16);
	kernel <<<blocks, threads >>>(d->dev_bitmap, ticks);

	cudaMemcpy(d->bitmap->get_ptr(),
		d->dev_bitmap,
		d->bitmap->image_size(),
		cudaMemcpyDeviceToHost
	);
}

int main(void)
{
	int count = 0;

	cudaGetDeviceCount(&count);

	if (!count)
	{
		std::cerr << "Not enough cuda devices =(" << std::endl;
		return 1;
	}

	cudaDeviceProp prop;
	cudaGetDeviceProperties(&prop, 0);
	
	DataBlock data;
	CPUAnimBitmap bitmap(DIMX, DIMY, &data);
	data.bitmap = &bitmap;

	cudaMalloc((void**)&data.dev_bitmap,
		bitmap.image_size()
	);
	bitmap.anim_and_exit((void(*)(void*, int))generate_frame,
		(void(*)(void*))cleanup
	);
	return 0;
}




/*
#include <GL\glut.h>

void display()
{
glClear(GL_COLOR_BUFFER_BIT);
glBegin(GL_LINES);
glColor3f(1.0, 0.0, 0.0);
glVertex2f(0.4, 0.4);
glVertex2f(0.4, 0.8);
glVertex2f(0.4, 0.8);
glVertex2f(0.8, 0.8);
glVertex2f(0.8, 0.8);
glVertex2f(0.8, 0.4);
glVertex2f(0.4, 0.4);
glVertex2f(0.8, 0.4);
glEnd();
glFlush();
}


int main(int argc, char **argv)
{
glutInit(&argc, argv);
glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
glutInitWindowSize(240, 240);
glutInitWindowPosition(100, 740);
glutCreateWindow("First window!");
glClearColor(1.0, 1.0, 1.0, 1.0);
glMatrixMode(GL_PROJECTION);
glLoadIdentity();
glOrtho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);
glutDisplayFunc(display);
glutMainLoop();

return 0;
}
*/