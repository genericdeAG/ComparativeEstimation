import { SprintDeletionComponent } from './product-owner/sprint-deletion/sprint-deletion.component';
import { AppRoutes } from './app.routing';
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
    AppComponent,
    EstimationComponent,
    SprintCreationComponent,
    SprintCreationSummaryComponent,
    WeightingResultsComponent,
    SprintDeletionComponent
],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutes
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
