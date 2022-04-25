package Loan;

/**
 *
 * @author Andre
 */

public class PersonalLoan extends Loan{ //Class derived from Loan    
    public PersonalLoan(int loanNum, int term, String lastName, double loan,
            double interestRate) //Public constructor
    {
       super(loanNum, term, lastName, loan);//Use of the constructor in the Loan class
       interest = interestRate + 2; //Personal Loan interest is 2% more than the current prime interest
    }
    @Override
    public void ToString() //Overriding the ToString method from the Loan class
    {
        System.out.println("Loan category:  Personal");
        System.out.println("Loan number:    " + loanNum);
        System.out.println("Last name:      " + lastName);
        System.out.println("Term:           " + term + " years");
        System.out.println("Loan:           R" + loan);
        System.out.println("Interest rate:  " + interest + "%");        
        System.out.println("Amount owed:    " + owed());        
    }    
}
