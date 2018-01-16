package smitrpz;

import static org.junit.Assert.*;

import org.junit.Test;
import smitrpz.home_work_6;

public class home_work_6Test {

	@Test
	public void test_1() {
		int [] arr = {1,2344, 23424, 1000, 2342342};
		int [] arrRes =  {1, 1000, 2344, 23424, 2342342};
		assertArrayEquals(arrRes, home_work_6.sortBySum(arr));
	}
	
	@Test
	public void test_2() {
		int [] arr = {1,2344, 100, 1000, 2342342};
		int [] arrRes =  {1, 100, 1000, 2344, 2342342};
		assertArrayEquals(arrRes, home_work_6.sortBySum(arr));
	}

	@Test
	public void test_3() {
		int [] arr = {15, 23 -47, 98, 13229, 896987, 65768};
		int [] arrRes =  {15, -24, 98, 13229, 65768, 896987};
		assertArrayEquals(arrRes, home_work_6.sortBySum(arr));
	}

}

