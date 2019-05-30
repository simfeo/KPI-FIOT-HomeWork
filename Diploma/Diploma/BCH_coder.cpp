#include "BCH_coder.h"
#include "MultXA.h"
#include "GaloisFielsNumber.h"
#include <math.h>
#include <vector>
#include <set>




static std::vector<MultXA> createPolynomialEquation(int fieldSize, int t)
{
	// find unique roots of g(x)
	std::vector<GaloisFielsNumber> galoisNumbers;
	std::set<unsigned int> minimalsCoefs;
	for (int i = 1; i < 2 * t + 1; ++i)
	{
		galoisNumbers.push_back(GaloisFielsNumber(fieldSize, GaloisFielsNumber::GetGaloisNumberFromPower(fieldSize, i)));
		minimalsCoefs.insert(galoisNumbers[i - 1].getMiminalPolinom());
	}

	//make polinomial multiplication of root
	std::vector<MultXA> polynom;
	for (auto& pol : minimalsCoefs)
	{
		std::vector<MultXA> tmpPolynom;
		for (int i = sizeof(pol) * 8; i >= 0; --i)
		{
			decltype(pol) mask = 1 << i;
			if (mask & pol)
			{
				tmpPolynom.push_back(MultXA(X(i)));
			}
		}
		if (polynom.empty())
		{
			polynom = tmpPolynom;
		}
		else
		{
			std::vector<MultXA> newPolynom;
			for (auto& el : polynom)
			{
				for (auto& el1 : tmpPolynom)
				{
					X x1(el.getPow());
					X x2(el.getPow());
					newPolynom.push_back(MultXA(MultXA::mult(x1, x2)));
				}
			}
			polynom = newPolynom;
		}
	}
	return polynom;
}



BCH_Encoder::BCH_Encoder(const int fieldSize, const int t):
	m_isSuccessful(true),
	m_fieldSize(fieldSize)
{
	int q = 2; //cause our code is binary
	int n = (int)pow(2, fieldSize)-1;

	if ((2 * t + 1) > n)
	{
		m_isSuccessful = false;
		return;
	}


	auto polynom = createPolynomialEquation(fieldSize, t);
	// find acutal g(x) for BCH
	unsigned int gx = 0; //g(x) Least common multiple
	for (auto el : polynom)
	{
		gx ^= 1 << el.getPow();
	}
	m_polynom = gx;

	// power of g(x)
	m_power = 0;
	for (int i = sizeof(gx) * 8; i >= 0; --i)
	{
		decltype(gx) mask = 1 << i;
		if (mask & gx)
		{
			m_power = i;
			break;
		}
	}
}

BCH_Encoder::BCH_Encoder(const BCH_Encoder & bchIn):
	m_power(bchIn.m_power),
	m_polynom(bchIn.m_polynom),
	m_isSuccessful(bchIn.m_isSuccessful),
	m_infoMessageLength(bchIn.m_infoMessageLength),
	m_fieldSize(bchIn.m_fieldSize)
{
}

bool BCH_Encoder::getIsSuccessful() const
{
	return m_isSuccessful;
}

int BCH_Encoder::getPower() const
{
	return m_power;
}

int BCH_Encoder::getMessageLength() const
{
	return m_infoMessageLength;
}

int BCH_Encoder::getTotalLength() const
{
	return m_power+m_infoMessageLength;
}
