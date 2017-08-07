import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { EstimationComponent } from './estimator/estimation/estimation.component';
import { SprintCreationComponent } from './product-owner/sprint-creation/sprint-creation.component';
import { SprintCreationSummaryComponent } from './product-owner/sprint-creation-summary/sprint-creation-summary.component';
import { WeightingResultsComponent } from './weighting/weighting-results/weighting-results.component';


@NgModule({
  declarations: [
    AppComponent
,
    EstimationComponent,
    SprintCreationComponent,
    SprintCreationSummaryComponent,
    WeightingResultsComponent
],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
