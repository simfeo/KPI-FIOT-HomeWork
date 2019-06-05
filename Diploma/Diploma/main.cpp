#include <iostream>
#include <vector>
#include <cmath>
#include <string>
#include <vector>
#include <iomanip>
#include <bitset>


#include "GaloisFieldNumber.h"

#include "BCH_coder.h"

int main(int argc, char** argv)
{
	int n = 4;
	std::vector<GaloisFieldNumber> numberVec;
	std::vector<std::string> numberVectorVec;
	int nSize = static_cast<int>(pow(2, n));
	for (int i =0; i < nSize; ++i)
	{
		if (GaloisFieldNumber::CheckGaloisParam(n, i))
		{
			numberVec.push_back(GaloisFieldNumber(n, i));
		}
		else
		{
			//std::cerr << "Error in creation Galois number size: " << n << " number: " << i << std::endl;
		}
	}

	/*
	for (auto& el : numberVec)
	{
		std::cout <<std::setw(4)<< el.getNumber() << " " <<std::setw(5)<< el.getBinaryView() << " " <<std::setw(20)<< el.getAlgAdiitStr()<<" "<<std::setw(3)<<el.getPower()<< " mp "<<std::bitset<8>(el.getMiminalPolinom())<<std::endl;
	}
	*/
	/*
	for (int i = 1; i < 15; ++i)
	{
		std::cout << "GF16 for power "<< i <<" = "  << GaloisFieldNumber(4, GaloisFieldNumber::GetGaloisNumberFromPower(4, i)).getNumber() << std::endl;
	}
	*/


	std::vector<unsigned char> sim = { 's','i','m'};

	BCH_Codec bb (4, 2);
	
	auto res = bb.Encode(sim);

	auto decoded = bb.Decode(res);

	std::cout<<(GaloisFieldNumber(4, GaloisFieldNumber::GetGaloisNumberFromPower(4, 12)) + GaloisFieldNumber(4, 1)).getPower()<<std::endl;

	return 0;
}
