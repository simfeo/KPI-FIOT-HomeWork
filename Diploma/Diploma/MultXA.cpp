#include "MultXA.h"



MultXA::MultXA(const A& a)
	: m_resultA(a.getPow()),
	m_resultX(1),
	m_shouldUnpack(true)
{

}

MultXA::MultXA(const X& x)
	: m_resultA(1),
	m_resultX(x.getPow()),
	m_shouldUnpack(false)
{

}

bool MultXA::getShouldUnpack()
{
	return m_shouldUnpack;
}

A MultXA::mult(A a1, A a2)
{
	return A(a1.getPow() + a2.getPow());
}

A MultXA::mult(A a1, X x1)
{
	return A(a1.getPow() + x1.getPow());
}

A MultXA::mult(X x1, A a1)
{
	return A(a1.getPow() + x1.getPow());
}

X MultXA::mult(X x1, X x2)
{
	return X(x1.getPow() + x2.getPow());
}

int MultXA::getPow()
{
	if (m_shouldUnpack)
		return m_resultA.getPow();
	else
		return m_resultX.getPow();
}