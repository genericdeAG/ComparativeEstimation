import { SprintDeletionComponent } from './product-owner/sprint-deletion/sprint-deletion.component';
import { SprintTotalweightingComponent } from './product-owner/sprint-totalweighting/sprint-totalweighting.component';
import { EstimationComponent } from './estimator/estimation/estimation.component';
import { SprintCreationSummaryComponent } from './product-owner/sprint-creation-summary/sprint-creation-summary.component';
import { SprintCreationComponent } from './product-owner/sprint-creation/sprint-creation.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    { path: "sprint-creation",              component: SprintCreationComponent },
    { path: "sprint-creation-summary/:id",  component: SprintCreationSummaryComponent },
    { path: "estimation/:id",               component: EstimationComponent },
    { path: "weighting-results/:id",        component: SprintTotalweightingComponent },
    { path: "sprint-deletion/:id",          component: SprintDeletionComponent },
    { path: "**",                           redirectTo: "sprint-creation" }
];
  
export const AppRoutes = RouterModule.forRoot(routes);