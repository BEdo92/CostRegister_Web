/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CostplansComponent } from './costplans.component';

describe('CostplansComponent', () => {
  let component: CostplansComponent;
  let fixture: ComponentFixture<CostplansComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CostplansComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CostplansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
