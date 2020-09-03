import { Component, OnInit } from '@angular/core';
import { Cost } from '../_models/Cost';
import { CostService } from '../_services/cost.service';
import { ActivatedRoute } from '@angular/router';
import { CategoryServiceService } from '../_services/category.service';
import { ShopService } from '../_services/shop.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BalanceService } from '../_services/balance.service';
import { RealCostFromPlan } from '../_models/RealCostFromPlan';

@Component({
  selector: 'app-costs',
  templateUrl: './costs.component.html',
  styleUrls: ['./costs.component.css']
})

export class CostsComponent implements OnInit {
  existingCosts: Cost[];
  categories: string[];
  shops: string[];
  costForm: FormGroup;
  newCost: Cost;
  balance: number;
  needToLoadCostPlans: boolean;
  plans: RealCostFromPlan[];
  idOfPlanToDelete: number;

  constructor(private costService: CostService,
    private routes: ActivatedRoute,
    private categoryService: CategoryServiceService,
    private shopService: ShopService,
    private fb: FormBuilder,
    private balanceService: BalanceService) { }

  ngOnInit() {
    this.createCostForm();

    this.loadPreviousCosts();
    this.loadCategories();
    this.loadShops();
    this.loadBalance();
  }

  createCostForm() {
    this.costForm = this.fb.group({
      dateOfCost: ['', Validators.required],
      formerPlans: [''],
      amountOfCost: ['', Validators.required],
      categoryName: [''],
      shopName: [''],
      additionalInformation: ['Megjegyzes', Validators.maxLength(100)]
    });
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe((categories: string[]) => {
      this.categories = categories;
    }, error => {
      alert(error);
    });
  }

  loadShops() {
    this.shopService.getShops().subscribe((shops: string[]) => {
      this.shops = shops;
    }, error => {
      alert(error);
    });
  }

  saveCost() {
    if (this.costForm.valid) {
      this.newCost = Object.assign({}, this.costForm.value);
      this.costService.addCost(this.newCost).subscribe(next => {
        console.log('Data saved successfully!');
        alert('Mentes sikeres!');
      }, error => {
        console.log('Failed to save data!');
        alert('Adatmentes sikertelen!');
      });

      console.log(this.newCost);
      this.loadBalance();

      // TODO: Delete the plan that was transformed into real cost
    }
    else {
      alert('Az urlap nincs megfeleloen kitoltve!');
    }
  }

  changeCategory(e) {
  }

  changeShop(e) {
  }

  loadPreviousCosts() {
    this.costService.getCosts().subscribe((existingCosts: Cost[]) => {
      this.existingCosts = existingCosts;
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

  loadPlans() {
    this.costService.getPlans().subscribe((plans: RealCostFromPlan[]) => {
      this.plans = plans;
    }, error => {
      alert(error);
    });
  }

  formerToggle() {
    this.needToLoadCostPlans = !this.needToLoadCostPlans;

    if (this.needToLoadCostPlans) {
      this.loadPlans();
    }
  }

  selectPlan() {
    // TODO: fill the form with values from the selected costplan.
    // save the id of the selected costplan in order to delete the plan when saving the cost
  }

}
