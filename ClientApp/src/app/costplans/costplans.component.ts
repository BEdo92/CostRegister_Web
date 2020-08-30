import { Component, OnInit } from '@angular/core';
import { CostPlan } from '../_models/CostPlan';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BalanceService } from '../_services/balance.service';
import { ActivatedRoute } from '@angular/router';
import { CostplanService } from '../_services/costplan.service';
import { CategoryServiceService } from '../_services/category.service';

@Component({
  selector: 'app-costplans',
  templateUrl: './costplans.component.html',
  styleUrls: ['./costplans.component.css']
})
export class CostplansComponent implements OnInit {

  categories: string[];
  existingCostPlans: CostPlan[];
  costPlanForm: FormGroup;
  newCostPlan: CostPlan;
  balance: number;

  constructor(private costPlanService: CostplanService,
    private routes: ActivatedRoute,
    private categoryService: CategoryServiceService,
    private fb: FormBuilder,
    private balanceService: BalanceService) { }

  ngOnInit() {
    this.createCostForm();

    this.loadPreviousCostPlans();
    this.loadCategories();
    this.loadBalance();
  }

  createCostForm() {
    this.costPlanForm = this.fb.group({
      typeOfCostPlan: ['', Validators.required],
      categoryName: [''],
      costPlanned: ['', Validators.required],
      planAdditionalInformation: ['Megjegyzes', Validators.maxLength(100)]
    });
  }


  saveCostPlan() {
    if (this.costPlanForm.valid) {
      this.newCostPlan = Object.assign({}, this.costPlanForm.value);
      this.costPlanService.addCostPlans(this.newCostPlan).subscribe(next => {
        console.log('Data saved successfully!');
        alert('Mentes sikeres!');
      }, error => {
        console.log('Failed to save data!');
        alert('Adatmentes sikertelen!');
      });

      console.log(this.newCostPlan);
      this.loadBalance();
    }
    else {
      alert('Az urlap nincs megfeleloen kitoltve!');
    }
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe((categories: string[]) => {
             this.categories = categories;
         }, error => {
           alert(error);
         });
  }

  changeCategory(e: any) {
    console.log(e);
  }

  loadPreviousCostPlans() {
    this.costPlanService.getCostPlans().subscribe((existingCostPlans: CostPlan[]) => {
      this.existingCostPlans = existingCostPlans;
    }, error => {
      alert(error);
    });
  }

  loadBalance() {
    this.balanceService.getBalance().subscribe((balance: number) => {
      this.balance = balance;
    }, error => {
      alert(error);
    });
  }


}
