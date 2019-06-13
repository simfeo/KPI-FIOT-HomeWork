#include "BCH_coder.h"
#include "MultXA.h"
#include "GaloisFieldNumber.h"
#include <math.h>
#include <vector>
#include <set>
#include <memory>

#define MAX_GALOIS_FILED_SIZE 28 // have no primitive polynoms for more then 28 in table

static std::vector<MultXA> createPolynomialEquation(int fieldSize, int t)
{
	// find unique roots of g(x)
	std::vector<GaloisFieldNumber> galoisNumbers;
	std::set<unsigned int> minimalsCoefs;
	for (int i = 1; i < 2 * t + 1; ++i)
	{
		galoisNumbers.push_back(GaloisFieldNumber(fieldSize, GaloisFieldNumber::GetGaloisNumberFromPower(fieldSize, i)));
		minimalsCoefs.insert(galoisNumbers[i - 1].getMiminalPolinom());
	}

	//make polinomial multiplication of root
	std::vector<MultXA> polynom;
	for (auto& pol : minimalsCoefs)
	{
		std::vector<MultXA> tmpPolynom;
		for (int i = sizeof(pol) * 8-1; i >= 0; --i)
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
			for (auto& el1 : polynom)
			{
				for (auto& el2 : tmpPolynom)
				{
					X x1(el1.getPow());
					X x2(el2.getPow());
					newPolynom.push_back(MultXA(MultXA::mult(x1, x2)));
				}
			}
			polynom = newPolynom;
		}
	}
	return polynom;
}

#pragma mark Encoder_Part

BCH_EncoderDecoder::BCH_EncoderDecoder(const int fieldSize, const int t) :
	m_isSuccessful(true),
	m_isDecodeSuccessful(false),
	m_fieldSize(fieldSize),
	m_maxErrorsNum(t)
{
	if (fieldSize > MAX_GALOIS_FILED_SIZE)
	{
		m_isSuccessful = false;
		return;
	}

	int q = 2; //cause our code is binary
	int n = (int)pow(2, fieldSize)-1;

	if (n > sizeof(unsigned long)*8)
	{
		m_isSuccessful = false;
		return;
	}

	if ((2 * t + 1) > n)
	{
		m_isSuccessful = false;
		return;
	}
	
	auto polynom = createPolynomialEquation(fieldSize, t);
	// find acutal g(x) for BCH
	unsigned long gx = 0; //g(x) Least common multiple
	for (auto el : polynom)
	{
		if (el.getPow() > (sizeof(m_polynom)*8 -2))
		{
			m_isSuccessful = false;
			return;
		}
		gx ^= 1 << el.getPow();
	}
	m_polynom = gx;

	// power of g(x)
	m_power = 0;
	for (int i = sizeof(gx) * 8 -1; i >= 0; --i)
	{
		decltype(gx) mask = 1 << i;
		if (mask & gx)
		{
			m_power = i;
			break;
		}
	}
	m_infoMessageLength = n - m_power;
}

BCH_EncoderDecoder::BCH_EncoderDecoder(const BCH_EncoderDecoder & bchIn):
	m_power(bchIn.m_power),
	m_polynom(bchIn.m_polynom),
	m_isSuccessful(bchIn.m_isSuccessful),
	m_isDecodeSuccessful(bchIn.m_isDecodeSuccessful),
	m_infoMessageLength(bchIn.m_infoMessageLength),
	m_fieldSize(bchIn.m_fieldSize),
	m_maxErrorsNum(bchIn.m_maxErrorsNum)
{
}

bool BCH_EncoderDecoder::getIsEncoderDecoderConstucted() const
{
	return m_isSuccessful;
}

bool BCH_EncoderDecoder::getIsLastDecodeSucessful() const
{
	return m_isDecodeSuccessful;
}

int BCH_EncoderDecoder::getPower() const
{
	return m_power;
}

int BCH_EncoderDecoder::getMessageLength() const
{
	return m_infoMessageLength;
}

int BCH_EncoderDecoder::getTotalLength() const
{
	return m_power+m_infoMessageLength;
}

unsigned long BCH_EncoderDecoder::encode(const unsigned long inMessage) const
{

	unsigned long Ra = inMessage; // division residue
	unsigned long pmShifted = m_polynom;
	unsigned long stopSign = 1 << getPower();
	while (true)
	{
		if ((Ra^pmShifted) > pmShifted)
		{
			pmShifted <<= 1;
			continue;
		}
		else
		{
			Ra ^= pmShifted;
			pmShifted = m_polynom;
		}
		if (Ra < stopSign)
		{
			break;
		}
	}

	unsigned long resMessage = 0;
	resMessage ^= Ra;
	resMessage ^= inMessage;
	return resMessage;

}

static unsigned long getRemainder(const unsigned long inMessage, const unsigned long polynom,  const unsigned long stopSign)
{

	unsigned long Ra = inMessage; // division residue
	unsigned long pmShifted = polynom;
	while (true)
	{
		if ((Ra^pmShifted) > pmShifted)
		{
			pmShifted <<= 1;
			continue;
		}
		else
		{
			Ra ^= pmShifted;
			pmShifted = polynom;
		}
		if (Ra < stopSign)
		{
			break;
		}
	}
	return Ra;
}


static unsigned int getDegree(unsigned long polynom)
{
	unsigned int deg = 0;
	for (unsigned long i = 0; i < sizeof(unsigned long) * 8 - 1; ++i)
	{
		unsigned long mask = 1 << i;
		if (polynom & mask)
		{
			++deg;
		}
	}
	return deg;
}


unsigned long BCH_EncoderDecoder::calcOrig(const unsigned long inMessage, int depth)
{
	unsigned long stopSign = 1 << getPower();
	unsigned long Ra = 0;
	for (unsigned long i = 0; i < (unsigned int)pow(2, m_fieldSize); ++i)
	{
		if (depth == 1)
		{
			if (i == 0)
			{
				Ra = getRemainder(inMessage, m_polynom, stopSign);
			}
			else
			{
				Ra = getRemainder(inMessage ^ (1 << (i-1)), m_polynom, stopSign);
			}
			if (Ra == 0)
			{	
				unsigned long out = 0;
				if (i == 0)
				{
					out = inMessage >> getPower();
				}
				else
				{
					out = (inMessage ^ (1 << i - 1)) >> getPower();
				}

				m_isDecodeSuccessful = true;
				return out;
			}
		}
		else
		{
			if (i == 0)
			{
				Ra = calcOrig(inMessage, depth - 1);
			}
			else
			{
				Ra = calcOrig(inMessage ^ (1 << (i-1)), depth - 1);
			}

			if (m_isDecodeSuccessful)
			{
				return Ra;
			}
		}
	}
	return 0;
}


unsigned long BCH_EncoderDecoder::decode(const unsigned long inMessage)
{
	m_isDecodeSuccessful = false;

	unsigned long stopSign = 1 << getPower();
	unsigned long Ra = getRemainder(inMessage, m_polynom, stopSign);

	
	if (Ra == 0)
	{
		m_isDecodeSuccessful = true; 
		unsigned long resMessage = 0;
		resMessage = inMessage >> getPower();

		return resMessage;
	}
	else
	{
		//unsigned long resMessage = 0;
		//return calcOrig(inMessage, m_maxErrorsNum);
		return tryToWithDecodeErrors(inMessage);
	}

	return 0;
}

unsigned long BCH_EncoderDecoder::tryToWithDecodeErrors(const unsigned long inMessage)
{
	std::vector<GaloisFieldNumber> syndromes;
	for (int j = 1; j < 2 * m_maxErrorsNum + 1; ++j)
	{
		unsigned long syndrome = 0;
		for (int i = 0; i < getTotalLength(); ++i)
		{
			unsigned long mask = 1 << i;
			if (mask&inMessage)
			{
				unsigned int power = (i * j) % ((unsigned int)pow(2, m_fieldSize) - 1);
				GaloisFieldNumber gfn (m_fieldSize, GaloisFieldNumber::GetGaloisNumberFromPower(m_fieldSize, power));
				syndrome ^= gfn.getNumber();
			}
		}
		syndromes.push_back(GaloisFieldNumber(m_fieldSize, syndrome));
	}

	std::vector<GaloisFieldNumber> dx;
	std::vector<std::vector<MultXaForBch>> cx, bx; //, tx;
	
	std::vector<MultXaForBch>tmp;
	tmp.push_back(MultXaForBch(A(0), X(0), m_fieldSize));
	cx.push_back(tmp);
	//cx.reserve(2 * m_maxErrorsNum + 1);
	for (int i = 0; i < 2 * m_maxErrorsNum; ++i)
	{
		cx.push_back(std::vector<MultXaForBch>());
	}
	bx = cx;


	unsigned int L = 0;
	for (int r = 1; r <= m_maxErrorsNum*2; ++r)
	{

		GaloisFieldNumber d = syndromes[r-1];

		if (r % 2)
		{

			for (int i = 1; i <= L; ++i)
			{
				unsigned int maxA = 0;
				for (auto el : cx[i])
				{
					maxA = maxA < el.getPowA() ? el.getPowA() : maxA;
				}
				GaloisFieldNumber g(m_fieldSize, GaloisFieldNumber::GetGaloisNumberFromPower(m_fieldSize, maxA));
				d = d + g * syndromes[r - i - 1];
			}
		}
		else
		{
			d = GaloisFieldNumber(m_fieldSize, 0); // every second d is 0, skip cals
		}

		if (d.getNumber() != 0)
		{
			MultXaForBch sd (d.getPower(), 0, m_fieldSize);
			MultXaForBch sx (0, 1, m_fieldSize);
			auto t = cx[r - 1]; // +d * GaloisFieldNumber(m_fieldSize, 2)*bx[r - 1];
			for (auto el : bx[r - 1])
			{
				t.push_back(sd* sx* el);
			}

			if (2*L <= r-1)
			{
				L = r-L;
				unsigned int dd = d.getPower();
				int dpow = static_cast<int>(sd.getPowA());
				dpow *= -1;
				dpow += ((unsigned int)pow(2, m_fieldSize) - 1);
				MultXaForBch sd1(dpow, 0, m_fieldSize);
				for (auto el : cx[r - 1])
				{
					bx[r].push_back(sd1*el);
				}

				cx[r] = t;
			}
			else
			{
				cx[r] = t;
				//bx[r] = GaloisFieldNumber(m_fieldSize, 2)*bx[r - 1];

				for (auto el : bx[r - 1])
				{
					bx[r].push_back(sx* el);
				}
			}
		}
		else
		{
			cx[r] = cx[r-1];
			MultXaForBch sx(0, 1, m_fieldSize);
			for (auto el : bx[r - 1])
			{
				bx[r].push_back(sx* el);
			}
			//bx[r] = GaloisFieldNumber(m_fieldSize, 2)*bx[r - 1];
		}

	}


	auto cxlast = cx[cx.size() - 1];
	std::vector<unsigned int> ans;
	
	unsigned int deg = 0;

	unsigned long  mess = inMessage;
	unsigned long stopSign = 1 << getPower();

	for (unsigned long i = 1; i < static_cast<unsigned int>(pow(2,m_fieldSize)); ++i)
	{
		GaloisFieldNumber res(m_fieldSize, 0);
		GaloisFieldNumber gf(m_fieldSize, i);
		for (auto el : cxlast)
		{
			GaloisFieldNumber x = GaloisFieldNumber(m_fieldSize, GaloisFieldNumber::GetGaloisNumberFromPower(m_fieldSize, el.getPowX()*i));
			GaloisFieldNumber alfa = GaloisFieldNumber(m_fieldSize, GaloisFieldNumber::GetGaloisNumberFromPower(m_fieldSize, el.getPowA()));
			res = res + x*alfa;
		}
		if (res.getNumber() == 0)
		{
			int dpow = i;
			dpow *= -1;
			dpow += ((unsigned int)pow(2, m_fieldSize) - 1);
			unsigned long mask = 1 << dpow;
			mess ^= mask;

			unsigned long Ra = getRemainder(mess, m_polynom, stopSign);

			if (Ra == 0)
			{
				m_isDecodeSuccessful = true;
				unsigned long resMessage = 0;
				resMessage = mess >> getPower();

				return resMessage;
			}
		}
	}
	

	return 0;
}

#pragma mark Codec_Part

BCH_Codec::BCH_Codec(const int fieldSize, const int t):
	m_bch(BCH_EncoderDecoder(fieldSize, t))
{
}

BCH_Codec::BCH_Codec(const BCH_EncoderDecoder & bch):
	m_bch(bch)
{

}

bool BCH_Codec::isEncoderSuccessfull() const 
{
	return m_bch.getIsEncoderDecoderConstucted();
}

unsigned char BCH_Codec::readBiteNum(const std::vector<unsigned char>& inMessage, unsigned int num) const
{
	if (num >= inMessage.size() * 8)
	{
		return false;
	}

	int vecpos = num / (sizeof(char) * 8);
	int vecshift = (8-(num % (sizeof(char) * 8)))-1;

	unsigned char mask = 1 << vecshift;
	return (mask & inMessage[vecpos]) ? 1 : 0;
}

void BCH_Codec::writeBiteNum(std::vector<unsigned char>& outMessage, unsigned int num, unsigned char value) const
{
	int vecpos = num / (sizeof(char) * 8);
	int vecshift = (8 - (num % (sizeof(char) * 8))) - 1;

	char mask = value << vecshift;
	outMessage[vecpos] = outMessage[vecpos] ^ mask;
}

void BCH_Codec::writeBiteNumToEmptyVec(std::vector<unsigned char>& outMessage, unsigned int num, unsigned char value) const
{
	int vecpos = num / (sizeof(char) * 8);
	int vecshift = (8 - (num % (sizeof(char) * 8))) - 1;

	if (outMessage.empty())
	{
		outMessage.push_back(0);
	}

	while (outMessage.size() <= vecpos)
	{
		outMessage.push_back(0);
	}
	
	char mask = value << vecshift;
	outMessage[vecpos] = outMessage[vecpos] ^ mask;
}


#include <iostream>
#include <bitset>

EncodedMessage BCH_Codec::Encode(const std::vector<unsigned char>& inMessage) const 
{
	EncodedMessage resMes;
	if (!m_bch.getIsEncoderDecoderConstucted())
	{
		resMes.totalLengthInBites = 0;
		return resMes;
	}

	resMes.totalLengthInBites = ((inMessage.size() * sizeof(char)*8) / m_bch.getMessageLength())*m_bch.getTotalLength();
	if ((inMessage.size() * sizeof(char) ) % m_bch.getMessageLength())
	{
		resMes.totalLengthInBites += m_bch.getTotalLength();
	}

	for (int i = 0; i < (resMes.totalLengthInBites + 7) / (sizeof(char)*8); ++i)
	{
		resMes.encodedMessage.push_back(char(0));
	}

	int currentPosRead = 0;
	int currentPosWrite = 0;

	while (currentPosWrite < (resMes.totalLengthInBites - 1))
	{
		unsigned long mess = 0;
		// read block
		for (int i = 0; i < m_bch.getMessageLength(); ++i)
		{
			mess = (mess << 1) ^ static_cast<unsigned long>(readBiteNum(inMessage, currentPosRead));
			++currentPosRead;
		}
		// encode block
		unsigned long res = m_bch.encode(mess<<m_bch.getPower());
		// store encoded data
		for (int i = m_bch.getTotalLength()-1; i >=0 ; --i)
		{
			unsigned long mask = 1 << i;

			writeBiteNum(resMes.encodedMessage, currentPosWrite, (res&mask) ? 1 : 0);
			++currentPosWrite;
		}
	}
	resMes.originalMessageLenghtInBites = inMessage.size()*8;
	return resMes;
}

std::vector<unsigned char> BCH_Codec::Decode(const EncodedMessage inMessage)
{
	if (!m_bch.getIsEncoderDecoderConstucted())
	{
		return std::vector<unsigned char>();
	}

	if (inMessage.encodedMessage.size() == 0 || inMessage.originalMessageLenghtInBites == 0)
	{
		return std::vector<unsigned char>();
	}

	std::vector<unsigned char> decodedMessage;

	int currentPosRead = 0;
	int currentPosWrite = 0;

	while (currentPosRead < (inMessage.totalLengthInBites - 1))
	{
		unsigned long mess = 0;
		// read block
		for (int i = 0; i < m_bch.getTotalLength(); ++i)
		{
			mess = (mess << 1) ^ static_cast<unsigned long>(readBiteNum(inMessage.encodedMessage, currentPosRead));
			++currentPosRead;
		}
		// encode block
		unsigned long res = m_bch.decode(mess);
		if (m_bch.getIsLastDecodeSucessful())
		{
			// store encoded data
			for (int i = m_bch.getMessageLength() - 1; i >= 0; --i)
			{
				unsigned long mask = 1 << i;

				writeBiteNumToEmptyVec(decodedMessage, currentPosWrite, (res&mask) ? 1 : 0);
				++currentPosWrite;
				if (currentPosWrite >= inMessage.originalMessageLenghtInBites)
				{
					break;
				}
			}
		}
		else
		{
			return std::vector<unsigned char>();
		}
	}
	return decodedMessage;
}

