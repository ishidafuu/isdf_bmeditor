<template>
  <div class="base-motion-container">
    <div class="character-view">
      <CharacterView :activeCellIndex="activeCellIndex" :offsetX="offsetX" :offsetY="offsetY" :flipX="flipX"
                     :flipY="flipY" :rotation="rotation"/>
      <BaseMotionListControl :storeName="'baseMotion'"/>
      <BaseMotionSaveLoadControl :storeName="'baseMotion'"/>
      <ErrorView/>
    </div>
    <div class="control-view">
      <BaseMotionController/>
    </div>
  </div>
</template>

<script>
import CharacterView from "../components/common/CharacterView.vue";
import BaseMotionController from "../components/motion/BaseMotionController.vue";
import BaseMotionListControl from "../components/motion/MotionListControl.vue";
import {mapState} from "vuex";
import BaseMotionSaveLoadControl from "../components/common/SaveLoadControl.vue";
import ErrorView from "../components/common/ErrorView.vue";
import KomaLoopControl from "../components/koma/KomaLoopControl.vue";


export default {
  name: 'BaseMotionEditor',
  components: {
    KomaLoopControl,
    ErrorView,
    BaseMotionSaveLoadControl,
    CharacterView,
    BaseMotionListControl,
    BaseMotionController,
  },
  computed: {
    ...mapState({
      activeCellIndex: state => state.baseMotion.motions[state.baseMotion.activeMotionIndex].komas[state.baseMotion.activeKomaIndex].cellNo,
      offsetX: state => state.baseMotion.motions[state.baseMotion.activeMotionIndex].komas[state.baseMotion.activeKomaIndex].offsetX,
      offsetY: state => state.baseMotion.motions[state.baseMotion.activeMotionIndex].komas[state.baseMotion.activeKomaIndex].offsetY,
      flipX: state => state.baseMotion.motions[state.baseMotion.activeMotionIndex].komas[state.baseMotion.activeKomaIndex].flipX,
      flipY: state => state.baseMotion.motions[state.baseMotion.activeMotionIndex].komas[state.baseMotion.activeKomaIndex].flipY,
      rotation: state => state.baseMotion.motions[state.baseMotion.activeMotionIndex].komas[state.baseMotion.activeKomaIndex].rotation,
    }),
  }
}
</script>

<style scoped>
.base-motion-container {
  display: flex; /* Flexboxを有効に */

}

.character-view {
  flex: 1; /* 左側の領域を均等に占める */
  min-width: 400px;
}

.control-view {
  flex: 1; /* 右側の領域を均等に占める */
  min-width: 600px;
}
</style>