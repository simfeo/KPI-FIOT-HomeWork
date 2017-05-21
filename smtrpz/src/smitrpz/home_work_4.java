package smitrpz;

import java.util.ArrayList;


public class home_work_4 {

//	Task 2
	static abstract class Animal
	{
		
	}
	
	static abstract class Mammal extends Animal
	{
		
	}
	
	public static class Lion extends Mammal
	{
		
	}
	
	static abstract class Hoofed extends Mammal
	{
		
	}
	
	public static class Zebra extends Hoofed
	{
		
	}
	
	public static class Girrafe extends Hoofed
	{
		
	}
	
	
	static abstract class Bird extends Animal
	{
		
	}
	
	public static class Eagle extends Bird
	{
		
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	static class Cage <E>
	{
		int maxSize;
		protected ArrayList<E> animals;
		
		Cage(int inMaxSize)
		{
			maxSize = inMaxSize;
			animals = new ArrayList<E>();
		}
		
		void AddAnimal(E an)
		{
			if (animals.size() < maxSize)
			{
				animals.add(an);
				return;
			}
			throw new RuntimeException("There is no space in this cage");
		}
		E RemoveAnimal()
		{
			if (animals.isEmpty())
				throw new RuntimeException("There is no animals in cage");
			
			E el = animals.get(animals.size()-1);
			animals.remove(animals.size()-1);
			return el;
		}
		
		int getMaxSize()
		{
			return maxSize;
		}
		int getCurrentLoad()
		{
			return animals.size();
		}
	}
	
	public static class Birdcage  extends Cage<Bird>
	{

		Birdcage(int inMaxSize) {
			super(inMaxSize);
			// TODO Auto-generated constructor stub
		}
		
	}
	
	static class MammalsCage extends Cage<Mammal>
	{

		MammalsCage(int inMaxSize) {
			super(inMaxSize);
			// TODO Auto-generated constructor stub
		}
		
	}
	
	
	public static class LionsCage extends Cage<Lion>
	{

		LionsCage(int inMaxSize) {
			super(inMaxSize);
			// TODO Auto-generated constructor stub
		}
		
	}
	
	public static class HoodedsCage extends Cage<Hoofed>
	{

		HoodedsCage(int inMaxSize) {
			super(inMaxSize);
			// TODO Auto-generated constructor stub
		}
		
	}
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		
		HoodedsCage c = new HoodedsCage(5);
		c.AddAnimal(new Zebra());
		c.AddAnimal(new Girrafe());
		c.RemoveAnimal();
		c.RemoveAnimal();
		c.RemoveAnimal();

	}

}
