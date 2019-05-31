#pragma once

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
	// get encoder state
	bool getIsSuccessful() const;
	// get power of minimal polinomial
	int	 getPower() const;
	// get how many bites can be encoded for iteration
	int  getMessageLength() const;
	// get lenght in bites for result info message
	int  getTotalLength() const;
	unsigned long encode(const unsigned long inVec) const;
private:
	// flag to check is decoder created correctly
	bool			m_isSuccessful;
	// gx - minimal polynomial for galois number
	unsigned long long	m_polynom;
	// size of galois field
	int				m_fieldSize;
	// power of gx
	int				m_power; 
	// lenght in bites how many bites of info message can be encoded
	int				m_infoMessageLength;
};


struct EncodedMessage
{
	std::vector<unsigned char>	encodedMessage;
	unsigned int		totalLengthInBites;
};


class BCH_Codec
{
public: 
	BCH_Codec(const int fieldSize, const int t);
	BCH_Codec(const BCH_Encoder& bch);
	bool isEncoderSuccessfull();
	EncodedMessage Encode(const std::vector<unsigned char>& inMessage);
private:
	unsigned char readBiteNum(const std::vector<unsigned char>& inMessage, unsigned int num) const;
	void writeBiteNum(std::vector<unsigned char>& outMessage, unsigned int num, unsigned char value);

	BCH_Encoder m_bch;
};
