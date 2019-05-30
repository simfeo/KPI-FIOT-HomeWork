#pragma once

#include <stdint.h>
#include <vector>

class BCH_Encoder
{
public:
	/*
	fieldSize is a power of Gaalois field that we want
	t is a count of wanted mistakes that can be fixed
	*/
	BCH_Encoder(const int fieldSize, const int t);
	BCH_Encoder(const BCH_Encoder& bchIn);
	bool getIsSuccessful() const;
	int	 getPower() const;
	int  getMessageLength() const;
	int  getTotalLength() const;
	std::vector<char> encode(const uint64_t inVec) const;
private:

	bool			m_isSuccessful;
	uint64_t		m_polynom;
	int				m_fieldSize;
	int				m_power;
	int				m_infoMessageLength;
};


struct EncodedMessage
{
	std::vector<char>	EncodedMessage;
	unsigned int		totalLengthInBites;
};


class BCH_Codec
{
public: 
	BCH_Codec(const int fieldSize, const int t);
	BCH_Codec(const BCH_Encoder& bch);
	bool isEncoderSuccessfull();
	EncodedMessage Encode(const std::vector<char> inMessage);
private:
	BCH_Encoder m_bch;
};
