package smitrpz;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.UUID;

public class home_work_3 {
// Task 2
	
	public interface AbstractTicketInterface
	{
		public boolean makePass();
		public boolean isBlocked();
		public void blockCard();
		public LocalDateTime getTimeCreated();
		public UUID getUUID();
	}

	public enum PaymentType
	{
		schoolchild,
		student,
		adult,
		nopaymenttype
	}
	
	public enum DurationType
	{
		fiveDay,
		month,
		noDuration
	}
	
	public enum CountType
	{
		five,
		ten,
		unlimited
	}
	
	public static class TicketBase implements AbstractTicketInterface
	{
		protected final UUID uid = UUID.randomUUID();
		protected LocalDateTime timeCreated = LocalDateTime.now();
		protected PaymentType paymentType;
		protected DurationType durationType;
		protected CountType countType;
        protected boolean blocked;

        public TicketBase() {
			blocked = false;
		}
		
		protected CountType getCountType()
		{
			return countType;
		}
		
		protected DurationType getDurationType()
		{
			return durationType;
		}
		
		public PaymentType getPaymentType()
		{
			return paymentType;
		}

		@Override
		public boolean makePass() {
			// TODO Auto-generated method stub
			return false;
		}

		@Override
		public LocalDateTime getTimeCreated() {
			// TODO Auto-generated method stub
			return timeCreated;
		}

		@Override
		public UUID getUUID() {
			return uid;
		}

		@Override
		public boolean isBlocked() {
			// TODO Auto-generated method stub
			return blocked;
		}

		@Override
		public void blockCard() {
			blocked = true;
		}
	}
	
	public static class LimitedCountedTicket extends TicketBase
	{
		private int count;
		public LimitedCountedTicket(CountType cnt, PaymentType payment)
		{
			super();
			switch (cnt)
			{
			case five:
				count = 5;
				break;
			case ten:
				count = 10;
				break;
			default:
				throw new RuntimeException("Wrong type for this type of ticket");
			};
			
			paymentType = payment;
			durationType = DurationType.noDuration;
			countType = cnt;
		}
		
		@Override
		public boolean makePass()
		{
			if (count == 0)
				return false;
			--count;
			return true;
		}
	}
	
	public static class LimitedTimeTicket extends TicketBase
	{
		private LocalDateTime startDay;
		private LocalDateTime lastDay;
		public LimitedTimeTicket(LocalDateTime startTime, DurationType dur, PaymentType payment)
		{
			super();
			switch (dur)
			{
			case fiveDay:
				lastDay = startTime.plusDays(5);
				durationType = DurationType.fiveDay;
				break;
			case month:
				durationType = DurationType.month;
				lastDay = startTime.plusMonths(1);
				break;
			default:
				throw new RuntimeException("Wrong type for this type of ticket");
			};
			
			startDay = startTime;
			paymentType = payment;
			countType = CountType.unlimited;
		}
		
		
		public LocalDateTime getStartDay() {
			return startDay;
		}
		
		public LocalDateTime getLastDay() {
			return lastDay;
		}
		
		@Override
		public boolean makePass()
		{
            LocalDateTime curTime = LocalDateTime.now();
            if(lastDay.isAfter(curTime) && startDay.isBefore(curTime)){
                return true;
            }
            return false;
		}
	}
	
	public static class LimitedBalanceTicket extends TicketBase
	{
		private int balance;
		public LimitedBalanceTicket(int stbalance, PaymentType payment)
		{
			super();
			if (stbalance <1)
				throw new RuntimeException("Balance shoudl be more then 1");
			balance = stbalance;
			paymentType = payment;
			countType = CountType.unlimited;
			durationType = DurationType.noDuration;
		}
		
		@Override
		public boolean makePass()
		{
            if(balance == 0)
	            return false;
            --balance;
            return true;
		}
		
		public void AddBalance(int count)
		{
			balance +=count;
		}
		
		public int GetBalance()
		{
			return balance;
		}
	}
	
	
	public static class Record
	{
		private UUID uid;
		private int balance;
		private LocalDateTime created;
		private LocalDateTime startTime;
		private LocalDateTime expiredTime;
		private DurationType durType;
		private CountType countType;
		private PaymentType paymentType;
		private int availableTrip;
		private boolean isSuccess;
		private String ticketType;
		
		public Record(TicketBase ticket, boolean success)
		{
			uid = ticket.getUUID();
			created = ticket.getTimeCreated();
			durType = ticket.getDurationType();
			countType = ticket.getCountType();
			paymentType = ticket.paymentType;
			
			availableTrip = -1;
			startTime = null;
			expiredTime = null;
			
			isSuccess = success;
		
			if (durType == DurationType.noDuration && countType == CountType.unlimited)
			{
				availableTrip = ((LimitedBalanceTicket)ticket).GetBalance();
				ticketType = "LimitedBalanceTicket";
			}
			else if (durType !=  DurationType.noDuration)
			{
				startTime = ((LimitedTimeTicket)ticket).getStartDay();
				expiredTime= ((LimitedTimeTicket)ticket).getLastDay();
				ticketType = "LimitedTimeTicket";
			}
			else
			{
				ticketType = "LimitedCountedTicket";
			}
		}
		private UUID getUid() {
			return uid;
		}
		private int getBalance() {
			return balance;
		}
		private LocalDateTime getCreatedTime() {
			return created;
		}
		private LocalDateTime getStartTime() {
			return startTime;
		}
		private LocalDateTime getExpiredTime() {
			return expiredTime;
		}
		private String getDurType() {
			
			switch (durType) {
			case fiveDay:
				return "5 day";
			case month:
				return "10 day";
			default:
				return "no limit";

			}
		}
		private String getCountType() {
			switch (countType)
			{
			case five:
				return "5";
			case ten:
				return "10";
			default:
				return "Unlimited";
			}
		}
		private String getPaymentType() {
			switch (paymentType) {
			case schoolchild:
				return "School";
			case student:
				return "Student";
			case adult:
				return "Full";
			default:
				return "no type";
			}
		}
		private int getAvailableTrip() {
			return availableTrip;
		}
		private boolean getIsSuccess()
		{
			return isSuccess;
		}
		
		public String geTicketType()
		{
			return ticketType;
		}
		
		public String getTotalInfo()
		{
			StringBuffer strBuf = new StringBuffer();
			strBuf.append("TicketType: " + geTicketType() + "\n");
			strBuf.append("UUID: "+getUid().toString()+"\n");
			strBuf.append("CreatedTime: "+getCreatedTime().toString()+"\n");
			strBuf.append("StartTime: "+getStartTime().toString()+"\n");
			strBuf.append("EndTime: "+getExpiredTime()+"\n");
			strBuf.append("Available trip count: "+getAvailableTrip()+"\n");
			strBuf.append("Was success: "+getIsSuccess()+"\n");
			strBuf.append("Payment type: "+getPaymentType()+"\n");
			strBuf.append("Count type: "+getCountType()+"\n");
			strBuf.append("Current balance:"+getBalance()+"\n");
			strBuf.append("Duartion type"+getDurType()+"\n");
			
			return strBuf.toString();
		}
		
		
	}
	
	public static class Wicket
	{
		private ArrayList<Record> createdList;
		private ArrayList<Record> recAttempList;
		public Wicket() {
			recAttempList = new ArrayList<Record>();
			createdList = new ArrayList<Record>();
		}
		
		public boolean tryToPass(TicketBase ticket)
		{
			boolean result = ticket.makePass();
			recAttempList.add(new Record(ticket, result));
			return result;
		}
		
		public ArrayList<Record> getRecordSuccess()
		{
			ArrayList<Record> rl = new ArrayList<Record>();
			for (Record i : recAttempList)
			{
				if (i.getIsSuccess());
					rl.add(i);
			}
			return rl;
		}
		
		public ArrayList<Record> getRecordfailed()
		{
			ArrayList<Record> rl = new ArrayList<Record>();
			for (Record i : recAttempList)
			{	
				if (! i.getIsSuccess())
					rl.add(i);
			}
			return rl;
		}
		
		LimitedCountedTicket createdLimitedCountedTicket(CountType cnt, PaymentType payment)
		{
			LimitedCountedTicket lct = new LimitedCountedTicket(cnt, payment);
			createdList.add(new Record(lct, false));
			return lct;
		}
		LimitedTimeTicket createdLimitedTimeTicket(LocalDateTime startTime, DurationType dur, PaymentType payment)
		{
			LimitedTimeTicket ltt = new LimitedTimeTicket(startTime, dur, payment);
			createdList.add( new Record(ltt, false));
			return ltt;
		}
		LimitedBalanceTicket createdLimitedBalanceTicket(int stbalance, PaymentType payment)
		{
			LimitedBalanceTicket lbt =  new LimitedBalanceTicket(stbalance, payment);
			createdList.add( new Record(lbt, false));
			return lbt;
		}
	}
	
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		
		Wicket wkt = new Wicket();
		
		LimitedTimeTicket ltt = wkt.createdLimitedTimeTicket(LocalDateTime.now().plusDays(1), DurationType.fiveDay, PaymentType.adult);
		LimitedBalanceTicket lbt = wkt.createdLimitedBalanceTicket(5, PaymentType.schoolchild);
		LimitedCountedTicket lct = wkt.createdLimitedCountedTicket(CountType.ten, PaymentType.student);
		
		wkt.tryToPass(ltt);
		wkt.tryToPass(lct);
		wkt.tryToPass(lbt);
		ArrayList<Record> failed = wkt.getRecordfailed();
		ArrayList<Record> success = wkt.getRecordSuccess();
		System.out.println(""+failed+"  "+success);
	}

}
