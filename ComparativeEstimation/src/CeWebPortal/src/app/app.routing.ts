import { WeightingResultsComponent } from './weighting/weighting-results/weighting-results.component';
import { EstimationComponent } from './estimator/estimation/estimation.component';
import { SprintCreationSummaryComponent } from './product-owner/sprint-creation-summary/sprint-creation-summary.component';
import { SprintCreationComponent } from './product-owner/sprint-creation/sprint-creation.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    { path: "sprint-creation", component: SprintCreationComponent },
    { path: "sprint-creation-summary", component: SprintCreationSummaryComponent },
    { path: "estimation", component: EstimationComponent },
    { path: "weighting", component: WeightingResultsComponent },
    { path: "", redirectTo: "sprint-creation", pathMatch: "full" },
    { path: "**", redirectTo: "sprint-creation", pathMatch: "full" }
  ];
  
  export const AppRoutes = RouterModule.forRoot(routes);