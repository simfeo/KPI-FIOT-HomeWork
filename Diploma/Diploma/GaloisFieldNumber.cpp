#include "GaloisFieldNumber.h"
#include "PrimitiveGroups.h"
#include "MultXA.h"
#include <math.h>
#include <algorithm>
#include <sstream>
#include <set>

/*
#include <iostream>
#include <bitset>
*/

#define MAX_GALOIS_FILED_SIZE 28 // have no primitive polynoms for more then 28 in table

bool GaloisFieldNumber::CheckGaloisParam(unsigned int fieldSize, unsigned int number)
{
	if (fieldSize < 2 || fieldSize > MAX_GALOIS_FILED_SIZE)
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

int GaloisFieldNumber::GetGaloisNumberFromPower(unsigned int fieldSize, unsigned int power)
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

GaloisFieldNumber::GaloisFieldNumber(unsigned int fieldSize, unsigned int number):
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

const unsigned int GaloisFieldNumber::getNumber() const
{
	return m_number;
}

const unsigned int GaloisFieldNumber::getPower() const
{
	return m_power;
}

const unsigned int GaloisFieldNumber::getFieldSize() const
{
	return m_gfSize;
}

const unsigned int GaloisFieldNumber::getMiminalPolinom() const
{
	return m_minimalPolinom;
}

const std::string GaloisFieldNumber::getBinaryView() const
{
	std::stringstream sbuf;
	for (unsigned int i =0; i < m_binary.size(); ++i)
	{
		sbuf << (m_binary[i] == 0 ? "0" : "1");
	}
	return sbuf.str();
}

const std::string GaloisFieldNumber::getAlgAdiitStr() const
{
	return m_algAdditStr;
}

GaloisFieldNumber GaloisFieldNumber::operator+(const GaloisFieldNumber & gfR)
{
	int m_new_num = m_number ^ gfR.getNumber();
	if (getFieldSize() == gfR.getFieldSize())
	{
		return GaloisFieldNumber(m_gfSize, m_new_num);
	}
	return GaloisFieldNumber(std::max(m_gfSize, gfR.getFieldSize()), m_new_num);
}

GaloisFieldNumber GaloisFieldNumber::operator-(const GaloisFieldNumber& gfR)
{
	return *this + gfR;
}

GaloisFieldNumber GaloisFieldNumber::operator*(const GaloisFieldNumber & gfR)
{
	if (getNumber() == 0 || gfR.getNumber() == 0)
	{
		return GaloisFieldNumber(getFieldSize(), 0);
	}

	int new_pow = getPower() + gfR.getPower();
	new_pow %= ((int)pow(2,getFieldSize())-1);
	int num = GetGaloisNumberFromPower(getFieldSize(), new_pow);
	return GaloisFieldNumber(getFieldSize(), num);
}

GaloisFieldNumber GaloisFieldNumber::operator/(const GaloisFieldNumber & gfR)
{
	int new_pow = getPower() - gfR.getPower();
	new_pow = new_pow > 0 ? new_pow : new_pow + (int)pow(2, getFieldSize())-1;
	int num = GetGaloisNumberFromPower(getFieldSize(), new_pow);
	return GaloisFieldNumber(getFieldSize(), num);
}

GaloisFieldNumber GaloisFieldNumber::power(unsigned int power)
{
	int new_pow = getPower() * power;
	new_pow = new_pow% ((int)pow(2, getFieldSize()) - 1);
	return GaloisFieldNumber(getFieldSize(), GaloisFieldNumber::GetGaloisNumberFromPower(getFieldSize(), new_pow));
}

void GaloisFieldNumber::calcPower()
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


static unsigned int multGF(unsigned int gfNumA, unsigned int gfNumB, unsigned int primitivePolynom)
{
	unsigned int production = 0;
	while (gfNumA && gfNumB)
	{
		if (gfNumB & 1)
			production ^= gfNumA;
		if (gfNumA & (1 << sizeof(unsigned int))) //overflow check
		{
			gfNumA = (gfNumA << 1) ^ primitivePolynom;
		}
		else
		{
			gfNumA <<= 1;
		}
		gfNumB >>= 1;
	}
	return production;
}


void GaloisFieldNumber::calcMiminal()
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
	int realSize = (int)pow(2, m);
	for (int i = 0; i < m; ++i)
	{
		int tmpPow = ((int)pow(p, i)) % realSize;
		cyclotomicClasses.insert((s * tmpPow) % (realSize - 1));
	}
	/*
	//std::sort(cyclotomicClasses.begin(), cyclotomicClasses.end());
	std::cout << "power " << s << " ";
	for (auto el : cyclotomicClasses)
	{
		std::cout << el << " ";
	}

	std::cout << std::endl;
	*/
	

	std::vector<MultXA> polynom;
	for (int num : cyclotomicClasses)
	{
		//std::cout << std::bitset<8>(galoisNum) << " ";
		if (polynom.empty())
		{
			polynom.push_back(MultXA(X(1)));
			polynom.push_back(MultXA(A(num)));
		}
		else
		{
			std::vector<MultXA> newPolynom;
			for (auto& el : polynom)
			{
				if (el.getShouldUnpack())
				{
					X x(1);
					A a(el.getPow());
					newPolynom.push_back(MultXA(MultXA::mult(x, a)));
				}
				else
				{
					X x1(1);
					X x2(el.getPow());
					newPolynom.push_back(MultXA(MultXA::mult(x1, x2)));
				}
			}

			for (auto& el : polynom)
			{
				if (el.getShouldUnpack())
				{
					A a1(num);
					A a2(el.getPow());
					newPolynom.push_back(MultXA(MultXA::mult(a1, a2)));
				}
				else
				{
					A a(num);
					X x(el.getPow());
					newPolynom.push_back(MultXA(MultXA::mult(a, x)));
				}
			}
			polynom = newPolynom;
		}
	}

	m_minimalPolinom = 0;
	for (auto el : polynom)
	{
		if (el.getShouldUnpack())
		{
			int pow = el.getPow() % (realSize - 1);
			m_minimalPolinom ^= GetGaloisNumberFromPower(getFieldSize(), pow);
		}
		else
		{
			m_minimalPolinom ^= 1 << el.getPow();
		}
	}

	//std::cout<<std::endl << "minimal is " << std::bitset<8>(m_minimalPolinom)<<std::endl;

}
