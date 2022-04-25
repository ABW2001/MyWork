/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package Loan;
import java.util.Scanner;
/**
 *
 * @author Andre
 */
public class CreateLoans {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        double primeInterest = 1;
        String loanType, lastName;
        int loanTerm, loanNum;
        double loanAmount;
        Loan[] loans = new Loan[5];//Array of business and personal loans
        Scanner input = new Scanner(System.in);//Scanner object
        
        System.out.println("What is the current prime interest?");
        primeInterest = input.nextDouble();//Receiving current prime interest rate with use of the Scanner object
        
        for (int i = 0; i < loans.length; i++)//Receiving loan information
        {
            System.out.println("What is the loan number?");
            loanNum = input.nextInt(); //Scanner object for input
            input.nextLine();//Ignores the new line character \n from when enter is pressed
            System.out.println("What is the loan type?");
            loanType = input.nextLine();//Scanner object for input
            System.out.println("What is the loan term?");
            loanTerm = input.nextInt();//Scanner object for input
            input.nextLine();//Ignores the new line character \n from when enter is pressed
            System.out.println("What is the last name?");
            lastName = input.nextLine();//Scanner object for input
            System.out.println("What is the loan amount?");
            loanAmount = input.nextDouble();//Scanner object for input         
            switch(loanType)//Ensures a valid loan type is selected
            {
                case "Business":
                    loans[i] = new BusinessLoan(loanNum, loanTerm, lastName, 
                            loanAmount, primeInterest);
                    break;
                case "Personal":
                    loans[i] = new PersonalLoan(loanNum, loanTerm, lastName, 
                            loanAmount, primeInterest);
                    break;
                default:
                    System.out.println("Invalid loan type");
            }            
        }
        for (Loan x : loans)//Displays all Business and Personal loans 
        {
            x.ToString();
        }
    }
    
}
