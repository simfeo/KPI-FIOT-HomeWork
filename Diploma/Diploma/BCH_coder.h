#pragma once

#include <vector>

class BCH_Encoder
{
public:
	BCH_Encoder(const int fieldSize, const int t);
	BCH_Encoder(const BCH_Encoder& bchIn);
	bool getIsSuccessful() const;
	int	 getPower() const;
	int  getMessageLength() const;
	int  getTotalLength() const;
	std::vector<char> encode(const std::vector<char>& inVec) const;
private:

	bool m_isSuccessful;
	unsigned int	m_polynom;
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
	BCH_Codec(const BCH_Encoder& bch);
	EncodedMessage Encode(std::vector<char> inMessage);
private:
	BCH_Encoder m_bch;
};
