#pragma once

#include <vector>

class BCH_EncoderDecoder
{
public:
	/*
	fieldSize is a power of Galois field that we want
	t is a count of wanted mistakes that can be fixed
	*/
	BCH_EncoderDecoder(const int fieldSize, const int t);
	BCH_EncoderDecoder(const BCH_EncoderDecoder& bchIn);
	// get encoder state
	bool getIsEncoderDecoderConstucted() const;
	// get encoder state
	bool getIsLastDecodeSucessful() const;
	// get power of minimal polinomial
	int	 getPower() const;
	// get how many bites can be encoded for iteration
	int  getMessageLength() const;
	// get lenght in bites for result info message
	int  getTotalLength() const;
	unsigned long encode(const unsigned long inMessage) const;
	unsigned long decode(const unsigned long inMessage);
private:
	// flag to check is encoder created correctly
	bool			m_isSuccessful;
	// flag to check is encoder created correctly
	bool			m_isDecodeSuccessful;
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
	unsigned int		totalLengthInBites = 0;
	unsigned int		originalMessageLenghtInBites = 0;
};


class BCH_Codec
{
public: 
	BCH_Codec(const int fieldSize, const int t);
	BCH_Codec(const BCH_EncoderDecoder& bch);
	bool isEncoderSuccessfull() const;
	EncodedMessage Encode(const std::vector<unsigned char>& inMessage) const;
	std::vector<unsigned char> Decode(const EncodedMessage inMessage);
private:
	unsigned char readBiteNum(const std::vector<unsigned char>& inMessage, unsigned int num) const;
	void writeBiteNum(std::vector<unsigned char>& outMessage, unsigned int num, unsigned char value) const;
	void writeBiteNumToEmptyVec(std::vector<unsigned char>& outMessage, unsigned int num, unsigned char value) const;

	BCH_EncoderDecoder m_bch;
};
