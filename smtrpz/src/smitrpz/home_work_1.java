package smitrpz;

import java.util.ArrayList;
import java.util.Arrays;

public class home_work_1 {

//	Task 3
//	Find words that contain only latin letters
// 	Among them find word that contains equal count of vowels and consonants.
//	Input: string line
// 	Output: string array
	
	public static boolean isVowel (char c)
	{
		 if(c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'y'
				    || c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U' || c == 'Y')
			 return true;
		 return false;
	}
	
	public static String [] getLatinWordsWithSameVowelsAndConsonants (String input)
	{
		String[] words = input.split("\\W+");
		ArrayList<String> result = new ArrayList<String>();
		for (String w : words)
		{
			int vowels =0;
			int consonants = 0;
			for (int i = 0; i< w.length(); i++)
			{
				if(isVowel(w.charAt(i)))
				{
					++vowels;
				}
				else
				{
					++consonants;
				}
			}
			if (vowels == consonants)
				result.add(w);
		}
		String[] resArr = new String[result.size()];
		resArr = result.toArray(resArr);
		return resArr;
	};
	

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		String s = "I want to walk my dog, cat, tarantula; maybe even my tortoise.";
		
		System.out.println(Arrays.toString(getLatinWordsWithSameVowelsAndConsonants(s)));
	}

}
