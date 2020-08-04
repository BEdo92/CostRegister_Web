import { Component, OnInit } from '@angular/core';
import { Cost } from '../_models/Cost';
import { CostService } from '../_services/cost.service';
import { ActivatedRoute } from '@angular/router';
import { CategoryServiceService } from '../_services/category.service';
import { ShopService } from '../_services/shop.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-costs',
  templateUrl: './costs.component.html',
  styleUrls: ['./costs.component.css']
})

export class CostsComponent implements OnInit {
  costs: Cost[];
  categories: string[];
  shops: string[];

  costForm: FormGroup;

  constructor(private costService: CostService,
    private routes: ActivatedRoute,
    private categoryService: CategoryServiceService,
    private shopService: ShopService) { }

  ngOnInit() {
    this.costForm = new FormGroup({
      costAmount: new FormControl('0 Ft', Validators.required),
      date: new FormControl(),
      categories: new FormControl(),
      shops: new FormControl(),
      additionalInfo: new FormControl('reszletek', Validators.maxLength(100))
    });
    
    this.routes.data.subscribe(data => {
      this.costs = data['costs'];
    });
    // this.loadPreviousCosts();
    this.loadCategories();
    this.loadShops();
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
    alert('Mentes sikeres!');
    console.log(this.costForm.value);
  }

   changeCategory(e) {
  }

  changeShop(e) {
  }

  // loadPreviousCosts() {
  //   this.costService.getCosts(+this.routes.snapshot.params['id']).subscribe((costs: Cost[]) => {
  //       this.costs = costs;
  //   }, error => {
  //     alert(error);
  //   });
  // }

}
