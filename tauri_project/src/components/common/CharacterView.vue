<template>
  <CrossLineView>
    <div class="cross-line-view">
      <BodyView :bodyX="bodyX" :bodyY="bodyY" :activeCellIndex="activeCellIndex" :flipX="flipX" :flipY="flipY"
                :rotation="rotation"/>
      <FaceView :faceIndex="faceIndex" :faceAngle="faceAngle"
                :bodyX="bodyX" :bodyY="bodyY"
                :faceX="faceX" :faceY="faceY" :faceZ="faceZ"
                :flipX="flipX" :flipY="flipY" :rotation="rotation"
      />
      <ItemView :itemAngle="itemAngle" :itemZ="itemZ" :itemX="itemX" :itemY="itemY" :flipX="flipX" :flipY="flipY"
                :rotation="rotation"/>
      <HitBoxView :hitX="hitX" :hitY="hitY" :hitW="hitW" :hitH="hitH"/>
      <HitBoxControl v-if="isEditableHitBox" :storeName="storeName"/>
    </div>
  </CrossLineView>
</template>

<script>
import {mapGetters, mapState, useStore} from 'vuex';
import CrossLineView from './CrossLineView.vue'
import BodyView from './BodyView.vue'
import FaceView from './FaceView.vue'
import ItemView from './ItemView.vue'
import HitBoxView from './HitBoxView.vue'
import HitBoxControl from '../attack/HitBoxControl.vue'
import {computed} from "vue";

export default {
  name: 'CharacterView',
  props: {
    activeCellIndex: {
      type: Number
    },
    offsetX: {
      type: Number,
      default: 0,
    },
    offsetY: {
      type: Number,
      default: 0,
    },
    flipX: {
      type: Number,
      default: 0,
    },
    flipY: {
      type: Number,
      default: 0,
    },
    rotation: {
      type: Number,
      default: 0,
    },
    hitX: {
      type: Number,
      default: 0,
    },
    hitY: {
      type: Number,
      default: 0,
    },
    hitW: {
      type: Number,
      default: 0,
    },
    hitH: {
      type: Number,
      default: 0,
    },
    isEditableHitBox: {
      type: Boolean,
      default: false,
    },
    storeName: {
      type: String,
      default: ''
    },
  },
  components: {
    CrossLineView,
    BodyView,
    FaceView,
    ItemView,
    HitBoxView,
    HitBoxControl,
  },
  setup(props) {
    const store = useStore()
    const bodyX = computed(() => store.state.charaCell.cells[props.activeCellIndex].bodyX + props.offsetX);
    const bodyY = computed(() => store.state.charaCell.cells[props.activeCellIndex].bodyY + props.offsetY);
    const faceIndex = computed(() => store.state.charaCell.cells[props.activeCellIndex].faceIndex);
    const faceAngle = computed(() => store.state.charaCell.cells[props.activeCellIndex].faceAngle);
    const faceX = computed(() => store.state.charaCell.cells[props.activeCellIndex].bodyX + store.state.charaCell.cells[props.activeCellIndex].faceX + props.offsetX);
    const faceY = computed(() => store.state.charaCell.cells[props.activeCellIndex].bodyY + store.state.charaCell.cells[props.activeCellIndex].faceY + props.offsetY);
    const faceZ = computed(() => store.state.charaCell.cells[props.activeCellIndex].faceZ);
    const itemAngle = computed(() => store.state.charaCell.cells[props.activeCellIndex].itemAngle);
    const itemZ = computed(() => store.state.charaCell.cells[props.activeCellIndex].itemZ);
    const itemX = computed(() => store.state.charaCell.cells[props.activeCellIndex].bodyX + store.state.charaCell.cells[props.activeCellIndex].itemX + props.offsetX);
    const itemY = computed(() => store.state.charaCell.cells[props.activeCellIndex].bodyY + store.state.charaCell.cells[props.activeCellIndex].itemY + props.offsetY);
    return {
      bodyX,
      bodyY,
      faceIndex,
      faceAngle,
      faceX,
      faceY,
      faceZ,
      itemAngle,
      itemZ,
      itemX,
      itemY,
    }
  }
}
</script>

<style scoped>
.cross-line-view {
  position: relative;
  width: 320px;
  height: 320px;
}


</style>