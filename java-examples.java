public class Examples {
    public static void main(String[] args) {
        
        // Positive/Negative & Even/Odd Check
        int number = -8;
        if (number > 0) {
            System.out.println("Positive");
            if (number % 2 == 0) {
                System.out.println("Even");
            }
            else {
                System.out.println("Odd");
            }
        }
        else if(number < 0) {
            System.out.println("Negative");
        }
        else {
            System.out.println("Number is 0");
        }
        
        // Grade Letter System
        int grade = 75;
        if(grade >= 90) {
            System.out.println("AA");
        }
        else if(grade >= 80) {
            System.out.println("BA");
        }
        else if(grade >= 70) {
            System.out.println("BB");
        }
        else if(grade >= 60) {
            System.out.println("CB");
        }
        else {
            System.out.println("Failed.");
        }
        
        // Login Authentication
        String username = "admin";
        String password = "1234";
        
        if(username.equals("admin")) {
            if (password.equals("1234")) {
                System.out.println("Login Successful, you're in.");
            } else {
                System.out.println("Password is wrong");
            }
        }
        else {
            System.out.println("Username is incorrect");
        }
        
        // Driver's License Age Check
        int age = 14;
        if (age >= 18) {
            System.out.println("You can get a driver's license.");
        }
        else {
            System.out.println("You're too young");
        }
        if (age < 12) {
            System.out.println("Ride a bike buddy");
        }
    }
}
