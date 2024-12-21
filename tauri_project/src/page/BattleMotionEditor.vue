<template>
  <div class="base-motion-container">
    <div class="character-view">
      <CharacterView :activeCellIndex="activeCellIndex" :offsetX="offsetX" :offsetY="offsetY" :flipX="flipX"
                     :flipY="flipY" :rotation="rotation" :hitX="hitX" :hitY="hitY" :hitW="hitW" :hitH="hitH"
                      :isEditableHitBox=true :storeName="'battleMotion'"/>
      <MotionListControl :storeName="'battleMotion'"/>
      <MotionSaveLoadControl :storeName="'battleMotion'"/>
      <ErrorView/>
    </div>
    <div class="control-view">
      <BattleMotionController/>
    </div>
  </div>
</template>

<script>
import {mapState} from "vuex";
import ErrorView from "../components/common/ErrorView.vue";
import CharacterView from "../components/common/CharacterView.vue";
import BattleMotionController from "../components/motion/BattleMotionController.vue";
import MotionListControl from "../components/motion/MotionListControl.vue";
import MotionSaveLoadControl from "../components/common/SaveLoadControl.vue";
import KomaLoopControl from "../components/koma/KomaLoopControl.vue";



export default {
  name: 'BattleMotionEditor',
  components: {
    KomaLoopControl,
    ErrorView,
    MotionSaveLoadControl,
    CharacterView,
    MotionListControl,
    BattleMotionController,
  },
  computed: {
    ...mapState({
      activeCellIndex: state => state.battleMotion.motions[state.battleMotion.activeMotionIndex].komas[state.battleMotion.activeKomaIndex].cellNo,
      offsetX: state => state.battleMotion.motions[state.battleMotion.activeMotionIndex].komas[state.battleMotion.activeKomaIndex].offsetX,
      offsetY: state => state.battleMotion.motions[state.battleMotion.activeMotionIndex].komas[state.battleMotion.activeKomaIndex].offsetY,
      flipX: state => state.battleMotion.motions[state.battleMotion.activeMotionIndex].komas[state.battleMotion.activeKomaIndex].flipX,
      flipY: state => state.battleMotion.motions[state.battleMotion.activeMotionIndex].komas[state.battleMotion.activeKomaIndex].flipY,
      rotation: state => state.battleMotion.motions[state.battleMotion.activeMotionIndex].komas[state.battleMotion.activeKomaIndex].rotation,
      hitX: state => state.battleMotion.motions[state.battleMotion.activeMotionIndex].komas[state.battleMotion.activeKomaIndex].attack.hitX,
      hitY: state => state.battleMotion.motions[state.battleMotion.activeMotionIndex].komas[state.battleMotion.activeKomaIndex].attack.hitY,
      hitW: state => state.battleMotion.motions[state.battleMotion.activeMotionIndex].komas[state.battleMotion.activeKomaIndex].attack.hitW,
      hitH: state => state.battleMotion.motions[state.battleMotion.activeMotionIndex].komas[state.battleMotion.activeKomaIndex].attack.hitH,
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