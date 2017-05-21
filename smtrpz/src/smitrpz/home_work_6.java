package smitrpz;

import java.util.Arrays;
import java.util.Comparator;

public class home_work_6 {
	
	 static class SumComparator implements Comparator<Integer>
	 {

		@Override
		public int compare(Integer arg0, Integer arg1) {
			// TODO Auto-generated method stub
			int sum0 = calcSum(arg0);
			int sum1 = calcSum(arg1);
			if ( sum0 > sum1)
				return 1;
			else if (sum0 < sum1)
				return -1;
			return 0;
		}
	 }
	
	public static int calcSum (int num)
	{
		int sum = 0;
		num = Math.abs(num);
		while(num/10 > 0 )
		{
			sum += num %10;
			num = num /10;
		}
		sum += num;
		return sum;
	}
	
	public static int [] sortBySum (int [] arr)
	{
		Integer[] sorted = new Integer [arr.length];
		int  i =0;
		for (int value : arr)
		{
			sorted[i] = value;
			i++;
		}
		Arrays.sort(sorted, new SumComparator());
		int [] res = new int [arr.length];
		i=0;
		for (Integer value : sorted)
		{
			res[i] = value;
			i++;
		}
		return res;
	}
	
	
	public static void main (String [] argv)
	{
		int [] arr = {15, 23 -47, 98, 13229, 896987, 65768};
		System.out.println(Arrays.toString(sortBySum(arr)));
	}
}
