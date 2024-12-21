<template>
  <div>
    <MotionNameControl :storeName="'battleMotion'"/>
    <KomaListControl :storeName="'battleMotion'"/>
    <KomaAddRemoveControl :storeName="'battleMotion'"/>
    <KomaCopyPasteControl :storeName="'battleMotion'"/>
    <KomaCellNoControl :storeName="'battleMotion'"/>
    <KomaOffsetControl :storeName="'battleMotion'"/>
    <KomaFlipRotationControl :storeName="'battleMotion'"/>
    <KomaFrameControl :storeName="'battleMotion'"/>
    <KomaActionControl :storeName="'battleMotion'"/>
    <KomaSeNoControl :storeName="'battleMotion'"/>
    <KomaLoopControl :storeName="'battleMotion'"/>
    <KomaHitControl :storeName="'battleMotion'"/>
    <UndoRedoControl :storeName="'battleMotion'"/>
    <SaveLoadControl :storeName="'battleMotion'"/>
  </div>
</template>

<script>
import MotionNameControl from "./MotionNameControl.vue";
import KomaListControl from "../koma/KomaListControl.vue";
import KomaAddRemoveControl from "../koma/KomaAddRemoveControl.vue";
import KomaCopyPasteControl from "../koma/KomaCopyPasteControl.vue";
import KomaCellNoControl from "../koma/KomaCellNoControl.vue";
import KomaOffsetControl from "../koma/KomaOffsetControl.vue";
import KomaFlipRotationControl from "../koma/KomaFlipRotationControl.vue";
import KomaFrameControl from "../koma/KomaFrameControl.vue";
import KomaActionControl from "../koma/KomaActionControl.vue";
import KomaSeNoControl from "../koma/KomaSeNoControl.vue";
import KomaLoopControl from "../koma/KomaLoopControl.vue";
import KomaHitControl from "../koma/KomaHitControl.vue";
import UndoRedoControl from "../common/UndoRedoControl.vue";
import SaveLoadControl from "../common/SaveLoadControl.vue";

export default {
  name: 'BattleMotionController',
  components: {
    SaveLoadControl, UndoRedoControl,
    MotionNameControl,
    KomaListControl,
    KomaAddRemoveControl,
    KomaCopyPasteControl,
    KomaCellNoControl,
    KomaOffsetControl,
    KomaFlipRotationControl,
    KomaFrameControl,
    KomaActionControl,
    KomaSeNoControl,
    KomaLoopControl,
    KomaHitControl,
  }
}
</script>