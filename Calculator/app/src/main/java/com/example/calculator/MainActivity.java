package com.example.calculator;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import androidx.appcompat.app.AppCompatActivity;

public class MainActivity extends AppCompatActivity {

    private TextView displayText;
    private double firstNumber = 0;
    private String operation = "";
    private boolean startNewNumber = true;

    @SuppressLint({"MissingInflatedId", "SetTextI18n"})
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        displayText = findViewById(R.id.displayText);

        View.OnClickListener digitListener = v -> {
            Button btn = (Button) v;
            if (startNewNumber) {
                displayText.setText(btn.getText().toString());
                startNewNumber = false;
            } else {
                displayText.append(btn.getText().toString());
            }
        };

        int[] digitIds = {
                R.id.btn0, R.id.btn1, R.id.btn2, R.id.btn3, R.id.btn4,
                R.id.btn5, R.id.btn6, R.id.btn7, R.id.btn8, R.id.btn9
        };

        for (int id : digitIds) {
            findViewById(id).setOnClickListener(digitListener);
        }

        findViewById(R.id.btnAdd).setOnClickListener(v -> prepareOperation("+"));
        findViewById(R.id.btnSubtract).setOnClickListener(v -> prepareOperation("-"));
        findViewById(R.id.btnMultiply).setOnClickListener(v -> prepareOperation("*"));
        findViewById(R.id.btnDivide).setOnClickListener(v -> prepareOperation("/"));
        findViewById(R.id.btnPower).setOnClickListener(v -> prepareOperation("^"));
        findViewById(R.id.btnFraction).setOnClickListener(v -> prepareOperation("/"));

        findViewById(R.id.btnSquareRoot).setOnClickListener(v -> {
            double value = Double.parseDouble(displayText.getText().toString());
            displayText.setText(String.valueOf(Math.sqrt(value)));
            startNewNumber = true;
        });

        findViewById(R.id.btnReciprocal).setOnClickListener(v -> {
            double value = Double.parseDouble(displayText.getText().toString());
            if (value != 0) {
                displayText.setText(String.valueOf(1 / value));
            } else {
                displayText.setText("Error");
            }
            startNewNumber = true;
        });

        findViewById(R.id.btnClear).setOnClickListener(v -> {
            displayText.setText("0");
            firstNumber = 0;
            operation = "";
            startNewNumber = true;
        });

        findViewById(R.id.btnEquals).setOnClickListener(v -> calculateResult());
    }

    private void prepareOperation(String selectedOperation) {
        firstNumber = Double.parseDouble(displayText.getText().toString());
        operation = selectedOperation;
        startNewNumber = true;
    }

    @SuppressLint("SetTextI18n")
    private void calculateResult() {
        double secondNumber = Double.parseDouble(displayText.getText().toString());
        double result = 0;

        switch (operation) {
            case "+": result = firstNumber + secondNumber; break;
            case "-": result = firstNumber - secondNumber; break;
            case "*": result = firstNumber * secondNumber; break;
            case "/":
                if (secondNumber != 0) result = firstNumber / secondNumber;
                else { displayText.setText("Error"); return; }
                break;
            case "^": result = Math.pow(firstNumber, secondNumber); break;
        }

        if (result == (long) result) {
            displayText.setText(String.valueOf((long) result));
        } else {
            displayText.setText(String.valueOf(result));
        }

        startNewNumber = true;
    }
}