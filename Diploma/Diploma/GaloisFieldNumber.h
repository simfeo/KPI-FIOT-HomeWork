#pragma once
#include <string>
#include <vector>

/*
 * This class represents galois number in filed size n
*/
class GaloisFieldNumber
{
public:
	// Check aparma of galois filed before creation
	static bool CheckGaloisParam(unsigned int fieldSize, unsigned int number);
	static int  GetGaloisNumberFromPower(unsigned int fieldSize, unsigned int power);
	GaloisFieldNumber(unsigned int fieldSize, unsigned int number);
	const unsigned int	getNumber() const;
	const unsigned int	getPower() const;
	const unsigned int	getFieldSize() const;
	const unsigned int	getMiminalPolinom() const;
	const std::string	getBinaryView() const;
	const std::string	getAlgAdiitStr() const;

	GaloisFieldNumber operator+(const GaloisFieldNumber& gfR);
	GaloisFieldNumber operator-(const GaloisFieldNumber& gfR);
	GaloisFieldNumber operator*(const GaloisFieldNumber& gfR);
	GaloisFieldNumber operator/(const GaloisFieldNumber& gfR);

private:
	void calcPower();
	void calcMiminal();

	unsigned int m_gfSize;
	unsigned int m_number;
	unsigned int m_power;
	unsigned int m_minimalPolinom;
	std::vector<char> m_binary;
	std::string m_algAdditStr;
	std::vector<std::string> m_algAdditVec;

};

