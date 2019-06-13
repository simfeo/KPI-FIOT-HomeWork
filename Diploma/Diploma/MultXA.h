#pragma once



class A //stub class for galois filed number in polynomial calculations
{
public:
	A(int pow) :m_pow(pow)
	{

	}
	const int getPow() const
	{
		return m_pow;
	}
private:
	int m_pow;
};

class X //stub class for x in polynomial calculations
{
public:
	X(int pow) :m_pow(pow)
	{

	}

	const int getPow() const
	{
		return m_pow;
	}
private:
	int m_pow;
};

class MultXA //class that responsible for multiplication of polinoms
{
public:
	MultXA(const A& a);
	MultXA(const X& x);

	bool getShouldUnpack();
	int getPow();

	static A mult(A a1, A a2);
	static A mult(A a1, X x1);
	static A mult(X x1, A a1);
	static X mult(X x1, X x2);
private:
	A		m_resultA;
	X		m_resultX;
	bool	m_shouldUnpack;
};

class MultXaForBch
{
public:
	MultXaForBch(const A& a, const X& x, unsigned int power);

	const unsigned int getPowA() const;
	const unsigned int getPowX() const;

	MultXaForBch operator*(const MultXaForBch& gfR);

private:
	unsigned int m_power;
	A		m_resultA;
	X		m_resultX;
	bool	m_shouldUnpack;
};