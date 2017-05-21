package smitrpz;

public class home_work_2 {

// Task 1
// Among simple number that a smaller then given number, find simple number
//	which contains maximal count of 1 in binary view

    public static boolean IsSimple(int n)
    {
        if(n < 1)
        {
            return false;
        }
        
        for(int i=2; i<n; i++)
        {
            if(n%i==0)
            {
                return false;
            }
        }
        return true;
    }  
	
	
	public static int getSimpleNumberWithMaximal_0b1(int maxNumber)
	{
		if (maxNumber <1)
			throw new RuntimeException("number should equal or bigger then 2");
		
		int max = 1;
		int countOf_0b1 = 1;
		int sizeOfInt = 32;
		
		for (int i =2; i< maxNumber; i++)
		{
			if (IsSimple(i))
			{
				int current_0b1 = 0;
				for (int k =0; k<sizeOfInt; ++k)
				{
					current_0b1 += ((1<<k)&i) != 0 ? 1 :0;
				}
				if(current_0b1 >= countOf_0b1)
				{
					countOf_0b1 = current_0b1;
					max = i;
				}
			}
		}
		
		return max;
		
	}
	
	
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		System.out.println(getSimpleNumberWithMaximal_0b1(17));
		System.out.println(getSimpleNumberWithMaximal_0b1(300));
	}

}
