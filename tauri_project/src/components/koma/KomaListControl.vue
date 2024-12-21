<template>
  <div class="koma-list-container">
    <ul class="koma-list-box">
      <li v-for="(koma, index) in komas" :key="koma.cellNo"
          @click="changeKomaIndex(index)"
          :class="{ 'selected': activeKomaIndex === index }">
        {{ index.toString().padStart(2, '0') }}: {{ koma.cellNo.toString().padStart(3, '0') }} {{ koma.frame }}f
        {{ getLoop(koma.loopPoint, koma.loopCount) }}
        {{ getSe(koma.seNo, koma.isLoopSe) }}
        {{ getAction(koma.isAction) }}
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import {defineComponent, computed} from 'vue'
import {useStore} from 'vuex'
import {LoopPoint} from '../../types/enum.ts';

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    },
  },
  setup(props) {
    const store = useStore()
    const activeKomaIndex = computed(() => store.state[props.storeName].activeKomaIndex)
    const komas = computed(() => store.state[props.storeName].motions[store.state[props.storeName].activeMotionIndex].komas)
    const changeKomaIndex = (index: number) => {
      store.commit(`${props.storeName}/changeKomaIndex`, index)
    }

    const getLoop = (loopPoint: number, loopCount: number) => {
      if (loopPoint > 0 && loopPoint < LoopPoint.LoopPointCount) {
        if (loopPoint === LoopPoint.Start
            || loopPoint === LoopPoint.SingleLoop) {
          return `${LoopPoint[loopPoint]}:${loopCount}`;
        }
        return `${LoopPoint[loopPoint]}`;
      } else {
        return "";
      }
    }

    const getSe = (seNo: number, isLoopSe: number) => {
      if (seNo > 0) {
        if (isLoopSe === 1) {
          return `LoopSe${seNo}`;
        }
        return `Se${seNo}`;
      } else {
        return "";
      }
    }

    const getAction = (isAction: number) => {
      if (isAction === 1) {
        return `Act`;
      } else {
        return "";
      }
    }

    return {
      komas,
      changeKomaIndex,
      activeKomaIndex,
      getLoop,
      getSe,
      getAction,
    }
  }
})
</script>


<style>
.koma-list-container {
  display: flex;
  justify-content: center;
}

.koma-list-box {
  height: 200px;
  width: 300px;
  overflow-y: auto;
  border: 1px solid #ccc;
  text-align: left;
}

.selected {
  background-color: #444;
}
</style>