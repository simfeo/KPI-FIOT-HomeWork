package smitrpz;
import java.awt.List;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
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
	
	class  TicketBase implements AbstractTicketInterface
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
	
	class LimitedCountedTicket extends TicketBase
	{
		private int count;
		public LimitedCountedTicket(CountType cnt, PaymentType payment)
		{
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
	
	class LimitedTimeTicket extends TicketBase
	{
		private LocalDateTime startDay;
		private LocalDateTime lastDay;
		public LimitedTimeTicket(LocalDateTime startTime, DurationType dur, PaymentType payment)
		{
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
	
	class LimitedBalanceTicket extends TicketBase
	{
		private int balance;
		public LimitedBalanceTicket(int stbalance, PaymentType payment)
		{
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
	
	
	class Record
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
			
			isSuccess = success
			
			
			if (durType == DurationType.noDuration && countType == CountType.unlimited)
			{
				availableTrip = ((LimitedBalanceTicket)ticket).GetBalance();
			}
			else if (durType !=  DurationType.noDuration)
			{
				startTime = ((LimitedTimeTicket)ticket).getStartDay();
				expiredTime= ((LimitedTimeTicket)ticket).getLastDay();
			}
		}
		public UUID getUid() {
			return uid;
		}
		public int getBalance() {
			return balance;
		}
		public LocalDateTime getCreatedTime() {
			return created;
		}
		public LocalDateTime getStartTime() {
			return startTime;
		}
		public LocalDateTime getExpiredTime() {
			return expiredTime;
		}
		public String getDurType() {
			
			switch (durType) {
			case fiveDay:
				return "5 day";
			case month:
				return "10 day";
			default:
				return "no limit";

			}
		}
		public String getCountType() {
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
		public String getPaymentType() {
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
		public int getAvailableTrip() {
			return availableTrip;
		}
		public boolean getIsSuccess()
		{
			return isSuccess;
		}
	}
	
	class Wicket
	{
		private ArrayList<Record> createdList;
		private ArrayList<Record> recAttempList;
		public Wicket() {
			recAttempList = new ArrayList<Record>();
		}
		
		public boolean tryToPass(TicketBase ticket)
		{
			boolean result = ticket.makePass();
			recAttempList.add(new Record(ticket, result));
			return result;
		}
		
		public int getRecordSuccess()
		{
			int count = 0
			for (Record i : recAttempList)
			{
				if (i.getIsSuccess())
					count++;
			}
			return count;
		}
		
		public int getRecordfailed()
		{
			int count = 0
			for (Record i : recAttempList)
			{
				if (! i.getIsSuccess())
					count++;
			}
			return count;
		}
		
		LimitedCountedTicket createdLimitedCountedTicket()
		{}
		LimitedTimeTicket createdLimitedTimeTicket()
		{}
		LimitedBalanceTicket createdLimitedBalanceTicket()
		{}
	}
	
	public static void main(String[] args) {
		// TODO Auto-generated method stub
	}

}
