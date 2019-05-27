#include "GaloisFielsNumber.h"
#include "PrimitiveGroups.h"
#include <math.h>
#include <algorithm>
#include <sstream>


bool GaloisFielsNumber::CheckGaloisParam(unsigned int fieldSize, unsigned int number)
{
	if (fieldSize < 2)
	{
		return false;
	}

	if (number == 0)
	{
		return false;
	}

	if (pow(2, fieldSize) - number <= 0)
	{
		return false;
	}

	return true;
}

GaloisFielsNumber::GaloisFielsNumber(unsigned int fieldSize, unsigned int number):
	m_gfSize(fieldSize),
	m_number(number)
{
	for (int i = 0; i <m_gfSize; ++i)
	{
		m_binary.push_back(m_number&(1 << i));
	}
	std::reverse(m_binary.begin(), m_binary.end());
	
	for (int i = 0; i < m_gfSize; ++i)
	{
		if (m_binary[i] == 0)
			continue;
		if (i == m_gfSize-1)
			m_algAdditVec.push_back("1");
		else if (i == m_gfSize-2)
			m_algAdditVec.push_back("x");
		else
			m_algAdditVec.push_back(std::string("x^(") + std::to_string(m_gfSize - i -1) + ")");
	}
	if (m_algAdditVec.empty())
	{
		m_algAdditVec.push_back("0");
	}

	if (m_algAdditVec.size() == 1)
	{
		m_algAdditStr = m_algAdditVec[0];
	}
	else
	{
		std::stringstream sbuf;
		for (unsigned int i = 0; i < m_algAdditVec.size(); ++i)
		{
			if (i != m_algAdditVec.size() - 1)
			{
				sbuf << m_algAdditVec[i] << "+";
			}
			else
			{
				sbuf << m_algAdditVec[i];
			}
			m_algAdditStr = sbuf.str();
		}
	}
	m_power = -1;
	if (m_number != 0)
	{
		for (int i = 0; i <= m_gfSize; ++i)
		{
			if ((m_number ^ (int)pow(2, i)) == 0)
			{
				m_power = i;
				break;
			}
		}
		if (m_power == -1)
		{
			m_power = m_number<<1 ^ primitive_polynoms::polynoms[m_gfSize];
			m_power %= (int)pow(2,m_gfSize);
		}
	}
}

const unsigned int GaloisFielsNumber::getNumber() const
{
	return m_number;
}

const unsigned int GaloisFielsNumber::getPower() const
{
	return m_power;
}

const unsigned int GaloisFielsNumber::getFieldSize() const
{
	return m_gfSize;
}

const std::string GaloisFielsNumber::getBinaryView() const
{
	std::stringstream sbuf;
	for (unsigned int i =0; i < m_binary.size(); ++i)
	{
		sbuf << (m_binary[i] == 0 ? "0" : "1");
	}
	return sbuf.str();
}

const std::string GaloisFielsNumber::getAlgAdiitStr() const
{
	return m_algAdditStr;
}

GaloisFielsNumber GaloisFielsNumber::operator+(const GaloisFielsNumber & gfR)
{
	int m_new_num = m_number ^ gfR.getNumber();
	if (getFieldSize() == gfR.getFieldSize())
	{
		return GaloisFielsNumber(m_gfSize, m_new_num);
	}
	return GaloisFielsNumber(std::max(m_gfSize, gfR.getFieldSize()), m_new_num);
}

GaloisFielsNumber GaloisFielsNumber::operator-(const GaloisFielsNumber& gfR)
{
	return *this + gfR;
}

GaloisFielsNumber GaloisFielsNumber::operator*(const GaloisFielsNumber & gfR)
{
	unsigned int production = 0;
	unsigned int gfNumA = getNumber();
	unsigned int gfNumB = getNumber();

	while (gfNumA && gfNumB)
	{
		if (gfNumB & 1)
			production ^= gfNumA;
		if (gfNumA & (1 << sizeof(unsigned int))) //overflow check
		{
			gfNumA = (gfNumA << 1) ^ primitive_polynoms::polynoms[getFieldSize()];
		}
		else
		{
			gfNumA <<= 1;
		}
		gfNumB >>= 1;
	}

	return GaloisFielsNumber(getFieldSize(), production);
}

GaloisFielsNumber GaloisFielsNumber::operator/(const GaloisFielsNumber & gfR)
{
	//quiet wrong
	return GaloisFielsNumber(3,3);
}
