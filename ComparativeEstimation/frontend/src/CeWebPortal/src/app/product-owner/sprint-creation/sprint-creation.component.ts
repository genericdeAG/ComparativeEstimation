import { RestProviderService } from './../../rest-provider/rest-provider.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { eConnectionStatus } from "app/eConnectionStatus";

@Component({
    selector: 'sprint-creation',
    templateUrl: './sprint-creation.component.html',
    styleUrls: ['./sprint-creation.component.css']
})
export class SprintCreationComponent {
    public eConnectionStatus = eConnectionStatus;
    emptyStory = "";

    stories: string[] = [];
    inputStory: string = this.emptyStory;
    changeStory: string = this.emptyStory;
    savedStoryOnChange = this.emptyStory;

    toggleFocusAddInput = false;
    toggleFocusChangeInput = false;

    isSendAllowed: boolean = false;
    connStatus: eConnectionStatus = eConnectionStatus.idle;

    constructor(
        private restProvider: RestProviderService,
        private router: Router) { }

    onAdd() {
        if (this.validateInput(this.inputStory.trim())) {
            this.stories.push(this.inputStory.trim());
            this.inputStory = this.emptyStory;
            this.setIsSendAllowed();
        }
        this.setFocus();
    }

    onDelete(index: number) {
        this.stories.splice(index, 1);
        this.setIsSendAllowed();
        this.setFocus();
    }

    onChangeStart(index: number) {
        this.savedStoryOnChange = this.stories[index];
        this.changeStory = this.stories[index];
        this.setFocus();
    }

    onChangeComplete(index: number) {
        this.stories.splice(index, 1);
        if (this.validateInput(this.changeStory.trim())) {
            this.savedStoryOnChange = this.emptyStory;
            this.stories.splice(index, 0, this.changeStory.trim());
        } else {
            this.stories.splice(index, 0, this.savedStoryOnChange);
        }
        this.setFocus();
    }

    onCreateSprint() {
        if (this.isSendAllowed) {
            this.setConnectionStatus(eConnectionStatus.sendInProgress);
            this.restProvider.createSprint(this.stories)
                .subscribe(
                    // Success
                    (id: string) => {
                        this.setConnectionStatus(eConnectionStatus.sendSuccess);
                        this.navigateToSprintCreationResult(id);
                    },
                    // Error
                    () => {
                        this.setConnectionStatus(eConnectionStatus.sendError);
                    });
        }
    }

    navigateToSprintCreationResult(id: string) {
        this.router.navigate(["/sprint-creation-summary", id]);
    }

    validateInput(input: string) {
        return (input && !this.stories.some(story => story == input))
    }

    setFocus() {
        if (this.savedStoryOnChange == this.emptyStory) {
            this.toggleFocusAddInput = !this.toggleFocusAddInput;
        } else {
            this.toggleFocusChangeInput = !this.toggleFocusChangeInput;
        }
    }

    setIsSendAllowed() {
        this.isSendAllowed = this.stories.length < 2 ? false : true &&
            ((this.connStatus == eConnectionStatus.idle)
                || (this.connStatus == eConnectionStatus.sendError));
    }

    setConnectionStatus(connStatus: eConnectionStatus) {
        this.connStatus = connStatus;
        this.setIsSendAllowed();
    }
}