import { Component, OnInit } from '@angular/core';
import { CardService, ProjectService, TestService } from '@app/services';
import { AddCardRequest, Card, GetCardRequest, GetProjectResponse, Phase } from '@app/models';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { AppNotify, DISPLAY_FORMAT_DATETIME, HEIGHT_BUTTON, PHASE_CODE, WIDTH_BUTTON } from '@app/utilities';

@Component({ selector: 'app-kanban', templateUrl: './kanban.component.html', styleUrls: ['./kanban.component.css'] })

export class KanbanComponent implements OnInit {

    lists: Phase[] = [];
    statuses: object[] = [
        {
            title: 'Cơ hội',
            color: '#8ccbbe',
            code: PHASE_CODE.Opportunity
        },
        {
            title: 'Báo giá',
            color: '#c56183',
            code: PHASE_CODE.Quote
        },
        {
            title: 'Đơn hàng',
            color: '#008891',
            code: PHASE_CODE.Order
        },
        {
            title: 'Hoàn thành',
            color: '#4cb043',
            code: PHASE_CODE.Completed
        }, {
            title: 'Hủy',
            color: 'red',
            code: PHASE_CODE.Destroy
        }
    ];

    private routeSub: Subscription;
    public employees: Object = {};
    kanbanComponent: KanbanComponent = this;
    withPopoverVisible: boolean = true;
    displayFormatDatetime = DISPLAY_FORMAT_DATETIME;
    project: GetProjectResponse = new GetProjectResponse({});
    isLoading: boolean = false;
    popupVisible: boolean = false;
    popupModalVisible: boolean = false;
    projectId: number;
    cardAdd: AddCardRequest = new AddCardRequest({});
    heightButton: number = HEIGHT_BUTTON;
    widthButton: number = WIDTH_BUTTON;
    isShowModal: boolean = false;
    card: Card = new Card({});


    constructor(
        private service: TestService,
        private route: ActivatedRoute,
        private projectService: ProjectService,
        private cardService: CardService,) {
        this.isLoading = true;
    }

    ngOnInit(): void {

        // Set projectId for new card
        this.routeSub = this.route.params.subscribe(params => {
            this.cardAdd.projectId = params['id'];
            this.projectId = params['id'];
        });

        this.loadProject();
        this.isLoading = false;

        this.loadKanban();

        console.log("KANBAN COMPONENT INIT");
    }

    onListReorder(e: any) {
        console.log("Reorder");
        const list = this.lists.splice(e.fromIndex, 1)[0];
        this.lists.splice(e.toIndex, 0, list);

        const status = this.statuses.splice(e.fromIndex, 1)[0];
        this.statuses.splice(e.toIndex, 0, status);

    }

    onTaskDragStart(e: any) {
        e.itemData = e.fromData[e.fromIndex];

    }

    async onTaskDrop(e: any, phaseId: number) {

        let canMove = await this.cardMove(e.itemData.id, phaseId);

        if (!canMove)
            return;

        e.fromData.splice(e.fromIndex, 1);
        e.toData.splice(e.toIndex, 0, e.itemData);
    }

    onCardClick(card: any, codePhase: string) {
        this.isLoading = true;
        this.card = new Card({});

        var cardRequest = new GetCardRequest({
            projectId: card.projectId,
            cardId: card.id
        });

        this.cardService.getCard(cardRequest).pipe(finalize(() => {
            this.isLoading = false
        })).subscribe(data => {
            this.card = data;
            this.card.phaseCode = codePhase;
            this.popupModalVisible = true;
        });
    }

    onClickAddNewCard() {
        console.log("Click add new");

        this.cardAdd.name = '';
        this.popupVisible = true;
    }

    onClickPriority() {
        console.log("onClickPriority()");
    }

    onActionPopup() {
        this.popupVisible = !this.popupVisible;
    }

    onClickCreateCard() {
        this.isLoading = true;

        this.cardService.addNewCard(this.cardAdd).pipe(finalize(() => {
            this.isLoading = false;
        })).subscribe(res => {

            AppNotify.success("Add new card success !");
            this.popupVisible = false;
            this.refreshKanban();
        });
    }

    loadProject() {
        this.isLoading = true;

        this.projectService.getOne(this.projectId).pipe(finalize(() => {
            this.isLoading = false;
        })).subscribe(res => {
            this.project = res;
        });
    }

    loadKanban() {
        this.isLoading = true;

        let listTemplate: Phase[] = []
        this.projectService.getPhaseAndListCard(this.projectId).pipe(finalize(() => {
            this.isLoading = false;
        })).subscribe(res => {
            this.statuses.forEach((status) => {

                let phase = res.find((phase) => phase.code === status['code']);

                status['phaseId'] = phase.id;
                listTemplate.push(phase);
            });

            this.lists = listTemplate;
        });
    }

    async cardMove(cardId: number, phaseId: number) {

        this.isLoading = true;

        return this.cardService.moveCard(cardId, phaseId).pipe(finalize(() => {
            this.isLoading = false;
        })).toPromise();
    }

    refreshKanban() {
        this.loadKanban();
    }

    onModalHidden(e) {
        console.log("OK");
    }
}
