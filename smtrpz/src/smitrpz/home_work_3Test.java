package smitrpz;

import static org.junit.Assert.*;

import java.time.LocalDateTime;
import java.util.ArrayList;

import org.junit.Test;
import smitrpz.home_work_3.*;

public class home_work_3Test {

	@Test
	public void test_1() {
		Wicket wkt = new Wicket();
		
		LimitedTimeTicket ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now().plusDays(1), DurationType.fiveDay, PaymentType.adult);
		LimitedBalanceTicket lbt = wkt.createdLimitedBalanceTicket(5, PaymentType.schoolchild);
		LimitedCountedTicket lct = wkt.createdLimitedCountedTicket(CountType.ten, PaymentType.student);
		
		wkt.tryToPass(ltt);
		wkt.tryToPass(lct);
		wkt.tryToPass(lbt);
	}
	
	@Test
	public void test_2() {
		Wicket wkt = new Wicket();
		
		LimitedTimeTicket ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now().plusDays(1), DurationType.fiveDay, PaymentType.adult);
				
		wkt.tryToPass(ltt);
		ArrayList<Record> failed = wkt.getRecordFailed();
		assertEquals(1, failed.size());
	}
	
	@Test
	public void test_3() {
		Wicket wkt = new Wicket();
		
		LimitedTimeTicket ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now().plusDays(1), DurationType.fiveDay, PaymentType.adult);
				
		wkt.tryToPass(ltt);
		ArrayList<Record> success = wkt.getRecordSuccess();
		assertEquals(0, success.size());
	}
	
	@Test
	public void test_4() {
		Wicket wkt = new Wicket();
		
		LimitedTimeTicket ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now().plusDays(1), DurationType.fiveDay, PaymentType.adult);
				
		wkt.tryToPass(ltt);
		ArrayList<Record> failed = wkt.getRecordFailed();
		assertEquals(true, failed.get(0).getIsTicketBlocked());
	}
	
	@Test  (expected=RuntimeException.class)
	public void test_5()
	{
		Wicket wkt = new Wicket();
		
		LimitedBalanceTicket lbt = wkt.createdLimitedBalanceTicket(5, PaymentType.schoolchild);
		lbt.AddBalance(-5);		
	}
	
	@Test  (expected=RuntimeException.class) 
	public void test_6()
	{
		Wicket wkt = new Wicket();
		LimitedCountedTicket lct = wkt.createdLimitedCountedTicket(CountType.unlimited, PaymentType.adult);
	}
	
	@Test  (expected=RuntimeException.class) 
	public void test_7()
	{
		Wicket wkt = new Wicket();
		LimitedTimeTicket ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now(), DurationType.noDuration, PaymentType.adult);
	}
	
	@Test
	public void test_8() {
		Wicket wkt = new Wicket();
		
		LimitedTimeTicket ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now().plusDays(1), DurationType.fiveDay, PaymentType.adult);
		LimitedBalanceTicket lbt = wkt.createdLimitedBalanceTicket(5, PaymentType.schoolchild);
		LimitedCountedTicket lct = wkt.createdLimitedCountedTicket(CountType.ten, PaymentType.student);
		
		wkt.tryToPass(ltt);
		wkt.tryToPass(lct);
		wkt.tryToPass(lbt);
		ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now().plusDays(10), DurationType.fiveDay, PaymentType.adult);
		wkt.tryToPass(ltt);
		ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now().plusDays(15), DurationType.month, PaymentType.adult);
		wkt.tryToPass(ltt);
		ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now().plusDays(20), DurationType.fiveDay, PaymentType.adult);
		wkt.tryToPass(ltt);
		assertEquals(6, wkt.getRecodeCreated().size());
	}

	@Test
	public void test_9() {
		Wicket wkt = new Wicket();
		
		LimitedTimeTicket ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now(), DurationType.fiveDay, PaymentType.adult);
	
		try {
			Thread.sleep(10);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		wkt.tryToPass(ltt);
		ArrayList<Record> success = wkt.getRecordSuccess();
		assertEquals(1, success.size());
	}
	
	@Test
	public void test_10() {
		Wicket wkt = new Wicket();
		
		LimitedTimeTicket ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now(), DurationType.fiveDay, PaymentType.adult);
		LimitedBalanceTicket lbt = wkt.createdLimitedBalanceTicket(5, PaymentType.schoolchild);
		
		wkt.tryToPass(ltt);
		wkt.tryToPass(lbt);
		ArrayList<Record> success = wkt.getRecordSuccess();
		assertEquals(2, success.size());
	}
}
