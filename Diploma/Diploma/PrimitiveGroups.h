#pragma once

#include <map>

/*

2 = x2+x+1
4 = x4+x+1
8 = x8+x4+x3+x2+x
16 = x16+x12+x3+x+1



*/


namespace primitive_polynoms
{

	int x2 = 1 << 2 + 1 << 1 + 1;
	int x3 = 1 << 3 + 1 << 1 + 1;
	int x4 = 1 << 4 + 1 << 1 + 1;
	int x5 = 1 << 5 + 1 << 2 + 1;
	int x6 = 1 << 6 + 1 << 1 + 1;
	int x7 = 1 << 7 + 1 << 3 + 1;
	int x8 = 1 << 8 + 1 << 4 + 1 << 3 + 1 << 2 + 1 << 1;
	int x9 = 1 << 9 + 1 << 4 + 1;
	int x10 = 1 << 10 + 1 << 3 + 1;
	int x11 = 1 << 11 + 1 << 2 + 1; 
	int x12 = 1 << 12 + 1 << 6 + 1 << 4 + 1 << 1 + 1;
	int x13 = 1 << 13 + 1 << 4 + 1 << 3 + 1 << 1 + 1;
	int x14 = 1 << 14 + 1 << 10 + 1 << 6 + 1 << 1 + 1;
	int x15 = 1 << 15 + 1 << 1 + 1;
	int x16 = 1 << 16 + 1 << 12 + 1 << 3 + 1 << 1 + 1;
	int x17 = 1 << 17 + 1 << 3 + 1;
	int x18 = 1 << 18 + 1 << 7 + 1;
	int x19 = 1 << 19 + 1 << 5 + 1 << 2 + 1 << 1 + 1;
	int x20 = 1 << 20 + 1 << 3 + 1;
	int x21 = 1 << 21 + 1 << 2 + 1;
	int x22 = 1 << 22 + 1 << 1 + 1;
	int x23 = 1 << 23 + 1 << 5 + 1;
	int x24 = 1 << 24 + 1 << 7 + 1 << 2 + 1 << 1 + 1;
	int x25 = 1 << 25 + 1 << 3 + 1;
	int x26 = 1 << 26 + 1 << 6 + 1 << 2 + 1 << 1 + 1;
	int x27 = 1 << 27 + 1 << 5 + 1 << 2 + 1 << 1 + 1;
	int x28 = 1 << 28 + 1 << 3 + 1;

	std::map<int, int> polynoms = { {	2	,	x2	}	,
									{	3	,	x3	}	,
									{	4	,	x4	}	,
									{	5	,	x5	}	,
									{	6	,	x6	}	,
									{	7	,	x7	}	,
									{	8	,	x8	}	,
									{	9	,	x9	}	,
									{	10	,	x10	}	,
									{	11	,	x11	}	,
									{	12	,	x12	}	,
									{	13	,	x13	}	,
									{	14	,	x14	}	,
									{	15	,	x15	}	,
									{	16	,	x16	}	,
									{	17	,	x17	}	,
									{	18	,	x18	}	,
									{	19	,	x19	}	,
									{	20	,	x20	}	,
									{	21	,	x21	}	,
									{	22	,	x22	}	,
									{	23	,	x23	}	,
									{	24	,	x24	}	,
									{	25	,	x25	}	,
									{	26	,	x26	}	,
									{	27	,	x27	}	,
									{	28	,	x28	}	
	};

};
