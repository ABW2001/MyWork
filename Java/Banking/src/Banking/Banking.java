package Banking;
/**
 *
 * @author Andre
 */
public class Banking {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        //Test accounts
        Account acc1 = new Account("A111", "John", 1000);
        Account acc2 = new Account("A222", "Mary", 500);        
        //Test withdrawal of 900 
        if(acc1.withdraw(900))
        {
            System.out.println("Withdraw OK, new Balance = " + acc1.getBalance());
        }
        else
        {
            System.out.println("Insufficient Funds. Current Balance = " 
                    + acc1.getBalance());
        }
    }    
}
class Account
{
    private String ID;
    private String Name;
    private double Balance;
    //Account constructor
    public Account(String ID, String Name, double Balance)
    {
        this.ID = ID;
        this.Balance = Balance;
        this.Name = Name;
    }
    //Get methods
    public String getID(){return ID;}
    public String getName(){return Name;}
    public double getBalance(){return Balance;}
    //Withdraw method
    public boolean withdraw(double amount)
    {
        if (Balance > amount)
        {
            Balance -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
    //Deposit method
    public void deposit(double amount)
    {
        Balance += amount;
    }
    //Transfer method
    public boolean transfer(Account dest, double amount)
    {
       if (withdraw(amount))
       {
           dest.Balance += amount;
           return true;
       } 
       else
       {
           return false;
       }
    }
}
