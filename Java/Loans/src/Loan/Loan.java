package Loan;

/**
 *
 * @author Andre
 */

 abstract class Loan implements LoanConstants{//Base class
    int loanNum, term;
    String lastName;
    double loan, interest;
    
    public abstract void ToString();//Abstract method that is overloaded in the BusinessLoan and PersonalLoan classes
    public double owed()
    {
        return loan + (loan / 100 * interest) * term; //Returns the total amount that is owed
    }
    
    public Loan(int loanNum, int term, String lastName, double loan)//Public constructor
    {
        //Set methods
        this.loanNum = loanNum;
        this.term = term;
        this.lastName = lastName;
        this.loan = loan;
        //Ensuring inputed term and loan amount is within the specified constraints
        if (term != shortTerm && term != mediumTerm && term != longTerm)
        {this.term = shortTerm;}
        if (loan > maxLoan){this.loan = maxLoan;}
    }
}
