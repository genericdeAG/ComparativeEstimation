import './rxjs-operators';

import { SprintDeletionComponent } from './product-owner/sprint-deletion/sprint-deletion.component';
import { AppRoutes } from './app.routing';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { RestProviderService } from './rest-provider/rest-provider.service';

import { AppComponent } from './app.component';
import { EstimationComponent } from './estimator/estimation/estimation.component';
import { SprintCreationComponent } from './product-owner/sprint-creation/sprint-creation.component';
import { SprintCreationSummaryComponent } from './product-owner/sprint-creation-summary/sprint-creation-summary.component';
import { SprintTotalweightingComponent } from './product-owner/sprint-totalweighting/sprint-totalweighting.component';
import { CePanelComponent } from './components/ce-panel/ce-panel.component';
import { CePanelContentComponent } from './components/ce-panel/ce-panel-content.component';
import { CePanelHeaderComponent } from './components/ce-panel/ce-panel-header.component';
import { CeFocusDirective } from './directives/ce-focus.directive';

@NgModule({
    declarations: [
    AppComponent,
    EstimationComponent,
    SprintCreationComponent,
    SprintCreationSummaryComponent,
    SprintDeletionComponent,
    SprintTotalweightingComponent,
    CePanelComponent,
    CePanelContentComponent,
    CePanelHeaderComponent,
    CeFocusDirective
],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutes
  ],
  providers: [
    RestProviderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
