<template>
  <div>
    <div>
      <button @click="changeLoopPoint(-1)">Decrease Loop Point</button>
      <span>Loop Point (X): {{ getLoopPointName(loopPoint)}}</span>
      <button @click="changeLoopPoint(1)">Increase Loop Point</button>
    </div>
    <div>
      <button @click="changeLoopCount(-1)">Decrease Loop Count</button>
      <span>Loop Count (Z,C): {{ loopCount }}</span>
      <button @click="changeLoopCount(1)">Increase Loop Count</button>
    </div>
  </div>
</template>

<script lang="ts">
import {defineComponent, computed, onMounted, onBeforeUnmount} from 'vue'
import {useStore} from 'vuex'
import {LoopPoint} from "../../types/enum.ts";

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    },
  },
  setup(props) {
    const store = useStore()

    const loopPoint = computed(() => store.getters[`${props.storeName}/currentKomaState`].loopPoint)
    const loopCount = computed(() => store.getters[`${props.storeName}/currentKomaState`].loopCount)

    const changeLoopPoint = (amount: number) => {
      store.commit(`${props.storeName}/changeLoopPoint`, amount)
    }

    const changeLoopCount = (amount: number) => {
      store.commit(`${props.storeName}/changeLoopCount`, amount)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (event.code === 'KeyX') {
        changeLoopPoint(1);
      } else if (event.code === 'KeyZ') {
        changeLoopCount(-1);
      } else if (event.code === 'KeyC') {
        changeLoopCount(1);
      }
    }

    const getLoopPointName = (value: number) => {
      if (value >= 0 && value < LoopPoint.LoopPointCount) {
        return LoopPoint[value];
      } else {
        return "";
      }
    };

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    })

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    })

    return {
      loopPoint,
      loopCount,
      changeLoopPoint,
      changeLoopCount,
      getLoopPointName,
    }
  }
})
</script>