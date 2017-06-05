package smitrpz;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.nio.charset.StandardCharsets;
import java.io.IOException;


public class home_work_5 {
//	Task 4
//	Convert byterstream to ut8
	
	
	public static void ConvertStream (InputStream inStr, OutputStream outStr)
	{
		char c ;
		InputStreamReader isr;		
		try {
			isr = new InputStreamReader(inStr, "windows-1251");
			c= (char) isr.read();
			while(c != -1)
			{
				String u8 = ""+c;
				outStr.write(u8.getBytes(StandardCharsets.UTF_8));
				c= (char) isr.read();
			}
			
		} 
		catch (UnsupportedEncodingException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}	
		catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	
	public static void main (String [] argv)
	{
		InputStream instr = System.in;
		OutputStream outstr = System.out;
		ConvertStream(instr, outstr);
	}

}
