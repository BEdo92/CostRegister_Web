import { Component, OnInit } from '@angular/core';
import { Cost } from '../_models/Cost';
import { CostService } from '../_services/cost.service';
import { ActivatedRoute } from '@angular/router';
import { CategoryServiceService } from '../_services/category.service';
import { ShopService } from '../_services/shop.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

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

  constructor(private costService: CostService,
    private routes: ActivatedRoute,
    private categoryService: CategoryServiceService,
    private shopService: ShopService,
    private fb: FormBuilder) { }

  ngOnInit() {
    this.createCostForm();

    this.loadPreviousCosts();
    this.loadCategories();
    this.loadShops();
  }

  createCostForm() {
    this.costForm = this.fb.group({
      dateOfCost: ['', Validators.required],
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

}
