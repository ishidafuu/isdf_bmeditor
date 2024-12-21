<template>
  <div>
    <EditStateControl/>
    <BodyIndexControl/>
    <BodyOffsetControl/>
    <FaceIndexControl/>
    <FaceAngleControl/>
    <FaceOffsetControl/>
    <FacePriorityControl/>
    <ItemAngleControl/>
    <ItemOffsetControl/>
    <ItemPriorityControl/>
    <UndoRedoControl :storeName="'charaCell'"/>
    <SaveLoadControl :storeName="'charaCell'"/>
  </div>
</template>

<script>
import EditStateControl from "./EditStateControl.vue";
import BodyIndexControl from "./BodyIndexControl.vue";
import BodyOffsetControl from "./BodyOffsetControl.vue";
import FaceIndexControl from "./FaceIndexControl.vue";
import FaceAngleControl from "./FaceAngleControl.vue";
import FaceOffsetControl from "./FaceOffsetControl.vue";
import FacePriorityControl from "./FacePriorityControl.vue";
import ItemAngleControl from "./ItemAngleControl.vue";
import ItemOffsetControl from "./ItemOffsetControl.vue";
import ItemPriorityControl from "./ItemPriorityControl.vue";
import UndoRedoControl from "../common/UndoRedoControl.vue";
import SaveLoadControl from "../common/SaveLoadControl.vue";

export default {
  name: 'CharaCellControlView',
  components: {
    EditStateControl,
    BodyIndexControl,
    BodyOffsetControl,
    FaceIndexControl,
    FaceAngleControl,
    FaceOffsetControl,
    FacePriorityControl,
    ItemAngleControl,
    ItemOffsetControl,
    ItemPriorityControl,
    UndoRedoControl,
    SaveLoadControl,
  }
}
</script>