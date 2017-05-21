package smitrpz;

import static org.junit.Assert.*;

import smitrpz.home_work_4.Birdcage;
import smitrpz.home_work_4.Girrafe;
import smitrpz.home_work_4.HoodedsCage;
import smitrpz.home_work_4.LionsCage;
import smitrpz.home_work_4.Zebra;
import smitrpz.home_work_4.Eagle;
import smitrpz.home_work_4.Lion;

import org.junit.Test;

public class home_work_4Test {

	@Test (expected=RuntimeException.class)
	public void test_1() {
		HoodedsCage c = new HoodedsCage(5);
		c.AddAnimal(new Zebra());
		c.AddAnimal(new Girrafe());
		c.RemoveAnimal();
		c.RemoveAnimal();
		c.RemoveAnimal();
	}
	
	@Test (expected=RuntimeException.class)
	public void test_2() {
		HoodedsCage c = new HoodedsCage(5);
		c.AddAnimal(new Zebra());
		c.AddAnimal(new Girrafe());
		c.AddAnimal(new Girrafe());
		c.AddAnimal(new Girrafe());
		c.AddAnimal(new Girrafe());
		c.AddAnimal(new Girrafe());
	}
	
	@Test 
	public void test_3()
	{
		Birdcage c = new Birdcage(5);
		c.AddAnimal(new Eagle());
		c.AddAnimal(new Eagle());
		c.AddAnimal(new Eagle());
		c.AddAnimal(new Eagle());
		c.AddAnimal(new Eagle());
	}
	
	@Test
	public void test_4()
	{
		LionsCage c = new LionsCage(4);
		c.AddAnimal(new Lion());
		c.AddAnimal(new Lion());
		c.RemoveAnimal();
		c.AddAnimal(new Lion());
		c.AddAnimal(new Lion());
		c.AddAnimal(new Lion());
	}
	
	
	@Test (expected=RuntimeException.class)
	public void test_5()
	{
		LionsCage c = new LionsCage(4);
		c.AddAnimal(new Lion());
		c.AddAnimal(new Lion());
		c.AddAnimal(new Lion());
		c.AddAnimal(new Lion());
		for (;;)
			c.RemoveAnimal();
	}

}
