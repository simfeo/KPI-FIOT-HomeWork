#include <iostream>
#include <vector>
#include <cmath>
#include <string>
#include <vector>
#include <iomanip>
#include <bitset>


#include "GaloisFielsNumber.h"

#include "BCH_coder.h"

int main(int argc, char** argv)
{
	int n = 4;
	std::vector<GaloisFielsNumber> numberVec;
	std::vector<std::string> numberVectorVec;
	int nSize = static_cast<int>(pow(2, n));
	for (int i =0; i < nSize; ++i)
	{
		if (GaloisFielsNumber::CheckGaloisParam(n, i))
		{
			numberVec.push_back(GaloisFielsNumber(n, i));
		}
		else
		{
			std::cerr << "Error in creation Galois number size: " << n << " number: " << i << std::endl;
		}
	}

	for (auto& el : numberVec)
	{
		std::cout <<std::setw(4)<< el.getNumber() << " " <<std::setw(5)<< el.getBinaryView() << " " <<std::setw(20)<< el.getAlgAdiitStr()<<" "<<std::setw(3)<<el.getPower()<< " minimal polynom "<<std::bitset<8>(el.getMiminalPolinom())<<std::endl;
	}

	std::vector<unsigned char> sim = { 's','i','m' };

	BCH_Codec bb (4, 2);
	
	auto res = bb.Encode(sim);

	auto decoded = bb.Decode(res);

	return 0;
}
