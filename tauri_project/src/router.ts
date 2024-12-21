import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router';
import CharaCellEditor from "./page/CharaCellEditor.vue";
import BaseMotionEditor from "./page/BaseMotionEditor.vue";
import BattleMotionEditor from "./page/BattleMotionEditor.vue";

const routes: Array<RouteRecordRaw> = [
    { path: '/', redirect: '/chara_cell_editor' },
    { path: '/chara_cell_editor', component: CharaCellEditor },
    { path: '/base_motion_editor', component: BaseMotionEditor },
    { path: '/battle_motion_editor', component: BattleMotionEditor }
];

const router = createRouter({
    history: createWebHashHistory(),
    routes
});

export default router;