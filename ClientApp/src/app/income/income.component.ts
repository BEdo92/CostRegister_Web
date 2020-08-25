import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Income } from '../_models/Income';
import { IncomeService } from '../_services/income.service';

@Component({
  selector: 'app-costs',
  templateUrl: './income.component.html',
  styleUrls: ['./income.component.css']
})

export class IncomeComponent implements OnInit {
  existingIncome: Income[];
  incomeForm: FormGroup;
  newIncome: Income;

  constructor(private incomeService: IncomeService,
    private routes: ActivatedRoute,
    private fb: FormBuilder) { }

  ngOnInit() {
    this.createIncomeForm();

    this.loadPreviousIncome();
  }

  createIncomeForm() {
    this.incomeForm = this.fb.group({
      typeOfIncome: ['', [Validators.required, Validators.maxLength(100)]],
      amountOfIncome: ['', Validators.required],
      dateOfIncome: ['', Validators.required],
      additionalInformation: ['Megjegyzes', Validators.maxLength(100)]
    });
  }

  saveIncome() {
    if (this.incomeForm.valid) {
        this.newIncome = Object.assign({}, this.incomeForm.value);
        this.incomeService.addIncome(this.newIncome).subscribe(next => {
          console.log('Data saved successfully!');
          alert('Mentes sikeres!');
        }, error => {
          console.log('Failed to save data!');
          alert('Adatmentes sikertelen!');
        });

        console.log(this.newIncome);
    }
  }


  loadPreviousIncome() {
    this.incomeService.getIncome().subscribe((existingIncome: Income[]) => {
        this.existingIncome = existingIncome;
    }, error => {
      alert(error);
    });
  }

}
