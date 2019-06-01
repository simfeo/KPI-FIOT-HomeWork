#include "BCH_coder.h"
#include "MultXA.h"
#include "GaloisFielsNumber.h"
#include <math.h>
#include <vector>
#include <set>
#include <memory>

#define MAX_GALOIS_FILED_SIZE 28 // have no primitive polynoms for more then 28 in table

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
	m_fieldSize(fieldSize)
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
	m_fieldSize(bchIn.m_fieldSize)
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
		if (Ra < m_polynom)
		{
			break;
		}
	}

	unsigned long resMessage = 0;
	resMessage ^= Ra;
	resMessage ^= inMessage;
	return resMessage;

}

unsigned long BCH_EncoderDecoder::decode(const unsigned long inMessage)
{
	m_isDecodeSuccessful = false;

	unsigned long Ra = inMessage; // division residue
	unsigned long pmShifted = m_polynom;
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
		if (Ra < m_polynom)
		{
			break;
		}
	}

	if (Ra == 0)
	{
		m_isDecodeSuccessful = true;
		unsigned long resMessage = 0;
		resMessage = inMessage >> getPower();
		return resMessage;
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

