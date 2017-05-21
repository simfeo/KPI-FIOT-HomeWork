package smitrpz;

import static org.junit.Assert.*;
import smitrpz.home_work_1;

import org.junit.Test;

public class home_work_1Test {

	@Test
	public void test1() {
		String testStr = "ssss sasasasa mmmm kaka";
		String [] result = home_work_1.getLatinWordsWithSameVowelsAndConsonants(testStr);
		assertEquals(result.length, 2);
	}

	
	@Test
	public void test2() {
		String testStr = "ssss asmos mmmm kaka";
		String [] result = home_work_1.getLatinWordsWithSameVowelsAndConsonants(testStr);
		assertEquals(result[0], "kaka");
	}
	
	@Test 
	public void test3()
	{
		String testStr = "111 22 333 mmmk3 soso3";
		String [] result = home_work_1.getLatinWordsWithSameVowelsAndConsonants(testStr);
		assertEquals(result.length, 0);
	}

	@Test 
	public void test4()
	{
		String testStr = "ababab bababa cycycycy";
		String [] result = home_work_1.getLatinWordsWithSameVowelsAndConsonants(testStr);
		assertEquals(result.length, 3);
	}
	
	@Test 
	public void test5()
	{
		String testStr = "ABABABAB BABABABABA CYDEMI";
		String [] result = home_work_1.getLatinWordsWithSameVowelsAndConsonants(testStr);
		assertEquals(result.length, 3);
	}
	
	@Test 
	public void test6()
	{
		String testStr = "Some random string from my mind";
		String [] result = home_work_1.getLatinWordsWithSameVowelsAndConsonants(testStr);
		assertEquals(result.length, 2);
	}
	
	
	@Test 
	public void test7()
	{
		String testStr = "Some-random:string=from my mind";
		String [] result = home_work_1.getLatinWordsWithSameVowelsAndConsonants(testStr);
		assertEquals(result.length, 2);
	}
	
	@Test 
	public void test8()
	{
		String testStr = "NOthingToLookHere";
		String [] result = home_work_1.getLatinWordsWithSameVowelsAndConsonants(testStr);
		assertEquals(result.length, 0);
	}
	
	@Test 
	public void test9()
	{
		String testStr = "ccc aaa ddd eee";
		String [] result = home_work_1.getLatinWordsWithSameVowelsAndConsonants(testStr);
		assertEquals(result.length, 0);
	}
	
	@Test 
	public void test10()
	{
		String testStr = "ssseee mmmiii kkk000";
		String [] result = home_work_1.getLatinWordsWithSameVowelsAndConsonants(testStr);
		assertEquals(result.length, 2);
	}
}
