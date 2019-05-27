#pragma once
#include <string>
#include <vector>

/*
 * This class represents galois number in filed size n
*/
class GaloisFielsNumber
{
public:
	// Check aparma of galois filed before creation
	static bool CheckGaloisParam(unsigned int fieldSize, unsigned int number);
	GaloisFielsNumber(unsigned int fieldSize, unsigned int number);
	const unsigned int	getNumber() const;
	const unsigned int	getPower() const;
	const unsigned int	getFieldSize() const;
	const std::string	getBinaryView() const;
	const std::string	getAlgAdiitStr() const;

	GaloisFielsNumber operator+(const GaloisFielsNumber& gfR);
	GaloisFielsNumber operator-(const GaloisFielsNumber& gfR);
	GaloisFielsNumber operator*(const GaloisFielsNumber& gfR);
	GaloisFielsNumber operator/(const GaloisFielsNumber& gfR);

private:
	unsigned int m_gfSize;
	unsigned int m_number;
	unsigned int m_power;
	std::vector<char> m_binary;
	std::string m_algAdditStr;
	std::vector<std::string> m_algAdditVec;

};

