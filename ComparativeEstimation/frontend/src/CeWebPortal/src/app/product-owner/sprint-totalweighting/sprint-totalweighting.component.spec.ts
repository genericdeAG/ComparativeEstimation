import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SprintTotalweightingComponent } from './sprint-totalweighting.component';

describe('SprintTotalweightingComponent', () => {
  let component: SprintTotalweightingComponent;
  let fixture: ComponentFixture<SprintTotalweightingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SprintTotalweightingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SprintTotalweightingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
