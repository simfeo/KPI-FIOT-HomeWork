package smitrpz;

import static org.junit.Assert.*;

import org.junit.Test;

public class home_work_2Test {

	@Test
	public void testGetSimpleNumberWithMaximal_0b1_01() {
		assertEquals(13, home_work_2.getSimpleNumberWithMaximal_0b1(15));
	}
	
	
	@Test (expected=RuntimeException.class)
	public void testGetSimpleNumberWithMaximal_0b1_02() {
		assertEquals(13, home_work_2.getSimpleNumberWithMaximal_0b1(-15));
	}
	
	@Test
	public void testGetSimpleNumberWithMaximal_0b1_03() {
		assertEquals(79, home_work_2.getSimpleNumberWithMaximal_0b1(100));
	}
	
	@Test
	public void testGetSimpleNumberWithMaximal_0b1_04() {
		assertEquals(191, home_work_2.getSimpleNumberWithMaximal_0b1(200));
	}
	
	@Test
	public void testGetSimpleNumberWithMaximal_0b1_05() {
		assertEquals(251, home_work_2.getSimpleNumberWithMaximal_0b1(300));
	}
	
	 
	@Test
	public void testIsSimple_1() {
		assertEquals(true, home_work_2.IsSimple(1));
	}
	
	@Test
	public void testIsSimple_2() {
		assertEquals(true, home_work_2.IsSimple(2));
	}
	
	@Test
	public void testIsSimple_3() {
		assertEquals(true, home_work_2.IsSimple(137));
	}
	
	@Test
	public void testIsSimple_4() {
		assertEquals(false, home_work_2.IsSimple(25));
	}
	
	@Test
	public void testIsSimple_5() {
		assertEquals(false, home_work_2.IsSimple(-12));
	}
	
}
