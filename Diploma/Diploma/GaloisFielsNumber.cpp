#include "GaloisFielsNumber.h"
#include "PrimitiveGroups.h"
#include <math.h>
#include <algorithm>
#include <sstream>
#include <iostream>
#include <set>


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

int GaloisFielsNumber::GetGaloisNumberFromPower(unsigned int fieldSize, unsigned int power)
{
	if (power < fieldSize)
	{
		return (int)pow(2,power);
	}
	else
	{
		power = 1 << power;
	}

	const unsigned int pm = primitive_polynoms::polynoms[fieldSize];

	unsigned int pmShifted = pm;
	while (true)
	{
		if ((power^pmShifted) > pmShifted)
		{
			pmShifted <<= 1;
			continue;
		}
		else
		{
			power ^= pmShifted;
			pmShifted = pm;
		}
		if (power < pm)
		{
			return power;
		}
	}

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
			calcPower();
		}

		calcMiminal();
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

const unsigned int GaloisFielsNumber::getMiminalPolinom() const
{
	return m_minimalPolinom;
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

void GaloisFielsNumber::calcPower()
{
	const unsigned int pm = primitive_polynoms::polynoms[getFieldSize()];
	unsigned int cand = 0;
	for (int i = getFieldSize(); i < (int)pow(2, getFieldSize()); ++i)
	{
		unsigned int pmShifted = pm;
		cand = 1 << i;
		while (true)
		{
			if ((cand^pmShifted) > pmShifted)
			{
				pmShifted <<= 1;
				continue;
			}
			else
			{
				cand ^= pmShifted;
				pmShifted = pm;
			}
			if (cand == getNumber())
			{
				m_power = i;
				return;
			}
			if (cand < pmShifted)
				break;
		}
	}
}

/*
static unsigned int multGF(unsigned int gfNumA, unsigned int gfNumB)
{
	unsigned int production = 0;
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
	return production;
}
*/

void GaloisFielsNumber::calcMiminal()
{
	std::set <int> cyclotomicClasses;
	int s = getPower();
	if (!s)
	{
		m_minimalPolinom = (1 << 1) + GetGaloisNumberFromPower(getFieldSize(), getPower());
		return;
	}

	int p = 2; // beacuse filed is binary
	int m = getFieldSize();
	for (int i = 0; i < m; ++i)
	{
		int tmpPow = ((int)pow(p, i)) % ((int)pow(2, m));
		cyclotomicClasses.insert((s*tmpPow) % (((int)pow(2, m)) -1));
	}

	//std::sort(cyclotomicClasses.begin(), cyclotomicClasses.end());
	std::cout << "power " << s << " ";
	for (auto el : cyclotomicClasses)
	{
		std::cout << el << " ";
	}

	std::cout << std::endl;
	

	m_minimalPolinom = 1;
	for (int num: cyclotomicClasses)
	{
		int galoisNum = GetGaloisNumberFromPower(getFieldSize(), num);
		//m_minimalPolinom *= (1+galoisNum)
	}

}
